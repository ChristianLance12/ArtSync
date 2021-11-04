import os

from flask import Flask, Blueprint
from flask_login import LoginManager 

from routes import unprotected, protected, admin, static_content
from datastore import User, db, init_db

from utils import get_scenes

IS_PRODUCTION = os.environ.get('NODE_ENV') == 'production'
DEBUG = True if not IS_PRODUCTION else False
PORT = 5000 if not IS_PRODUCTION else os.environ.get('PORT')
SECRET_KEY = 'tacocat' if not IS_PRODUCTION else os.environ.get('SECRET_KEY')
DATABASE_URL = os.environ.get('DATABASE_URL') if IS_PRODUCTION else 'sqlite:///database.db'




class FlaskApp(Flask):
    def run(self, host=None, port=None, debug=None, load_dotenv=True, **options):
        with self.app_context():
            init_db(IS_PRODUCTION)    
        super(FlaskApp, self).run(host=host, port=port, debug=debug, load_dotenv=load_dotenv, **options)

app = Flask(__name__, static_folder='build')
app.config['SECRET_KEY'] = SECRET_KEY
app.config['SQLALCHEMY_DATABASE_URI'] = DATABASE_URL
app.config['SCENES'] = get_scenes()

BASE_FOLDER = os.path.dirname(os.path.abspath(__file__))
UPLOAD_FOLDER = BASE_FOLDER + '/static_content/uploads'
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

# static folder for react app build
build_folder = Blueprint('build', __name__, static_url_path='/', static_folder='build')
app.register_blueprint(build_folder)

app.register_blueprint(protected)
app.register_blueprint(unprotected)
app.register_blueprint(admin)
app.register_blueprint(static_content, url_prefix='/static_content')

db.init_app(app)

login_manager = LoginManager()
login_manager.init_app(app)

@login_manager.user_loader
def load_user(user_id):
    return User.query.get(int(user_id))


if __name__ == '__main__':
    print('::: {}'.format(PORT))
    app.run(debug=DEBUG, host='0.0.0.0', port=PORT)


