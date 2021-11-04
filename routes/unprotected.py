import json

from flask import Blueprint, request, render_template, current_app
from flask_login import login_user, current_user
from datastore import get_user_by_email, create_user
from werkzeug.security import check_password_hash
from utils import get_uploads

unprotected = Blueprint('unprotected', __name__, template_folder='../build')

@unprotected.errorhandler(404)
def page_not_found():
    return render_template('index.html')



@unprotected.route('/')
def index():
    return render_template('index.html', )


@unprotected.route('/login', methods=['POST'])
def login():
    email = request.form.get('email')
    password = request.form.get('password')

    user = get_user_by_email(email)

    if not user:
        return json.dumps({ "success": False, "message": "User with this email does not exist" });

    login_success = check_password_hash(user.password, password)

    if not login_success: 
        return json.dumps({ "success": False, "message": "Incorrect username or password" });

    login_user(user, remember=True)
    return json.dumps({ "success": True, "message": "Hello, {}".format(user.name) });



@unprotected.route('/register', methods=['POST'])
def register():

    email = request.form.get('email')
    name = request.form.get('name')
    password = request.form.get('password')

    user = get_user_by_email(email)

    if user:
        return json.dumps({ "success": False, "message": "{} is already used".format(email) });

    new_user = create_user(name, email, password)
    if new_user:
        login_user(new_user)
        return json.dumps({ "success": True, "message": "succesfully registered" });

    return json.dumps({ "success": False, "message": "Failed to register" });



@unprotected.route('/user_is_authorized')
def data():
    return json.dumps({ "authorized": current_user.is_authenticated})    



@unprotected.route('/scenes')
def scenes():
    scenes = current_app.config['SCENES']
    return json.dumps({ "scenes": scenes })    


@unprotected.route('/uploads')
def uploads():
    uploads = get_uploads()
    return json.dumps({ "uploads": uploads })    