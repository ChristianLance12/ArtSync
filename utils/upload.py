import os


ALLOWED_EXTENSIONS = {'txt', 'pdf', 'png', 'jpg', 'jpeg', 'gif'}
REL_UPLOADS_FOLDER = "static_content/uploads"

def allowed_file(filename):
    return '.' in filename and \
           filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


def get_uploads():
    try:
        uploads = []
        for file in os.listdir(REL_UPLOADS_FOLDER):
            filepath = os.path.join(REL_UPLOADS_FOLDER, file)
            uploads.append(filepath)

        return uploads

    except Exception as e:
        print("ERROR GETTING UPLOADS")
        print(e)
