import os

from flask import Blueprint, send_from_directory, current_app, request
from werkzeug.utils import secure_filename
from utils import allowed_file

static_content = Blueprint('static_content', __name__)

@static_content.before_request
# @login_required
def before_request():
    # TODO: check if user has permissions to file
    # if not, return 404
    
    # print(current_user)
    pass


@static_content.route('/')
@static_content.route('/<path:path>')
def send_js(path):
    return send_from_directory('static_content', path)


@static_content.route('/upload', methods=['POST'])
def upload_file():

    if 'file' not in request.files:
        return None

    file = request.files['file']

    if file.filename == '':
        return None

    if file and allowed_file(file.filename):
        filename = secure_filename(file.filename)
        filepath = os.path.join( current_app.config['UPLOAD_FOLDER'], filename)
        file.save(filepath)

        return filepath
