from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from twilio.rest import Client
import random
import datetime

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = "sqlite:(DATABASE_LOCATION)"
db = SQLAlchemy(app)



class Bank(db.Model):
    account = db.Column(db.Integer, unique=True, nullable=False, primary_key=True)
    name = db.Column(db.String, unique=False, nullable=False)
    balance = db.Column(db.Float, unique=False, nullable=False)
    mobile = db.Column(db.Integer, unique=True, nullable=False)
    email = db.Column(db.String, unique=False, nullable=False)
    DOB = db.Column(db.String, unique=False, nullable=False)
    Father = db.Column(db.String, unique=False, nullable=False)
    CustomerId = db.Column(db.Integer, unique=True, nullable=False)
    password = db.Column(db.Integer, unique=False, nullable=False)
    cash = db.Column(db.Integer, unique=False, nullable=False)



class OTP(db.Model):
    id = db.Column(db.Integer, unique=True, nullable=False, primary_key=True)
    CustomerId = db.Column(db.Integer, unique=False, nullable=False)
    OTP = db.Column(db.Integer, unique=False, nullable=False)
    identification = db.Column(db.Integer, unique=False, nullable=False)
    time = db.Column(db.Integer, unique=False, nullable=False)




class Status:
    """Gives the status of the account"""
    def __init__(self):
        self.bank = Bank()
        self.otp = OTP()
        self.selected_account = None
        self.account_sid = "Your_Accound_Sid"
        self.auth_token = "Your_Authentication_token"
        self.client = Client(self.account_sid, self.auth_token)


    def authenticate(self, user, pin):
        """Authenticating the user"""
        self.selected_account = None
        with app.app_context():
            self.selected_account = self.bank.query.filter_by(CustomerId=user).first()
        if self.selected_account is not None:
            if int(pin) == self.selected_account.password:
                name = self.selected_account.name
                return name
            else:
                self.selected_account = None
                return "Wrong"
        else:
            print("Authenticate: No Account found")
            return None

    def check_balance(self, user, pin):
        """Check the balance of the user"""
        self.selected_account = None
        check = self.authenticate(user=user, pin=pin)
        if check is None or check == "Wrong":
            return check
        else:
            with app.app_context():
                result = Bank.query.filter_by(CustomerId=self.selected_account.CustomerId).first()
            return result.balance

    def otp_req_pin(self, user, pin):
        """Change the pin of the user"""
        self.selected_account = None
        check = self.authenticate(user=user, pin=pin)
        if check is None or check == "Wrong":
            return check
        else:
            with app.app_context():
                result = Bank.query.filter_by(CustomerId=self.selected_account.CustomerId).first()
            otp_rand = random.randint(1000, 9999)
            identity = random.randint(10000, 99999)
            self.message_otp(otp=otp_rand, number=result.mobile)
            with app.app_context():
                print(identity)
                new_otp = OTP(CustomerId=result.CustomerId, OTP=otp_rand, identification=identity, time=int(datetime.datetime.now().strftime("%Y%m%d%H%M%S")))
                db.session.add(new_otp)
                db.session.commit()
                print("Commited")
            return identity

    def message_otp(self, otp, number):
        """Sent the otp to the mobile"""
        self.client.messages.create(body=f"Greetings from jorgan\nYour OTP is {otp}.\nNever share with anyone",
                                    to=f"+91{number}", from_="+16203878285")

    def pin_change(self, otp, new_pin, identity):
        """Changes the pin"""
        with app.app_context():
            print(identity)
            print(type(identity))
            result = OTP.query.filter_by(identification=int(identity)).first()
            print(result)
        if result is None:
            return "something wrong"
        elif int(otp) == result.OTP:
            if (int(datetime.datetime.now().strftime("%Y%m%d%H%M%S")) - int(result.time)) > 300:
                db.seesion.delete(result)
                db.session.commit()
                return "OTP Expired"
            elif (int(datetime.datetime.now().strftime("%Y%m%d%H%M%S")) - int(result.time)) < 300:
                with app.app_context():
                    new = Bank.query.filter_by(CustomerId=result.CustomerId).first()
                    print(new)
                    print(type(new))
                    db.session.delete(result)
                    new.password = new_pin
                    db.session.commit()
                self.selected_account = None
                return "Pin changed"
        else:
            return "Wrong"

    def money_change(self, identity, amount):
        """Changes the money in the account"""
        with app.app_context():
            account = Bank.query.filter_by(CustomerId=identity).first()
            account.balance += amount
            self.message_amount(balance=account.balance, amount=amount, number=amount.mobile)
            db.session.commit()

    def message_amount(self, balance, amount, number):
        """Sent the message about the transaction"""
        transaction_type = ""
        if int(amount) < 0:
            transaction_type = "debit"
            amount = amount * (-1)
        else:
            transaction_type = "credit"
        self.client.messages.create(body=f"Greetings from jorgan\nAn amount of INR {amount} has been "
                                         f"{transaction_type}ed in your account.\nThe available balance is {balance}",
                                    to=f"+91{number}", from_="+16203878285")

    def check_cash(self, user, pin):
        """Check the balance of the user"""
        self.selected_account = None
        check = self.authenticate(user=user, pin=pin)
        if check is None or check == "Wrong":
            return check
        else:
            with app.app_context():
                result = Bank.query.filter_by(CustomerId=self.selected_account.CustomerId).first()
            return result.cash


