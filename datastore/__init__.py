import json

from .db import db
from .user import *
from .user_words import *
from .words import *
from .spanish import *
from .english import *

def init_db(production=False):

    if production:
        return

    db.drop_all()
    db.create_all()



