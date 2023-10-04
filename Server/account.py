import smtplib
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from twilio.rest import Client
import random
import datetime

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = "sqlite:///jorgon.db"
db = SQLAlchemy(app)
my_email = "azhagesan807@gmail.com"
passwords = "xyujmdlgnquguoou"

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

class Verification(db.Model):
    id = db.Column(db.Integer, unique=True, nullable=False, primary_key=True)
    identification = db.Column(db.String, unique=False, nullable=False)
    email_OTP = db.Column(db.Integer, unique=False, nullable=False)
    mobile_OTP = db.Column(db.Integer, unique=False, nullable=False)
    account = db.Column(db.Integer, unique=True, nullable=False)

def accountno(mobile):
    with app.app_context():
        query = Bank.query.all()
        part_query = Bank.query.filter_by(mobile=mobile).all()

        for j in part_query:
            if j.CustomerId == 0:
                db.session.delete(j)
                db.session.commit()
        customerid = [i.account for i in query]
    while True:
        account_no = random.randint(9500000000, 10000000000)
        if account_no not in customerid:
            break
    return account_no

class Status:
    """Gives the status of the account"""
    def __init__(self):
        self.bank = Bank()
        self.otp = OTP()
        self.selected_account = None
        self.account_sid = "AC5587b4f57348f5e0472afaddfc3d3a34"
        self.auth_token = "6af3bbb1b2fe5dc1d861fa399b405433"
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

    def newUser(self, name, father, dob, number, email):
        """Creates New User"""
        account = accountno(mobile=number)
        new_user = Bank(name=name, balance=0, Father=father, mobile=number, email=email, cash=0, DOB=dob,
                        account=account, CustomerId=0, password=0)
        newOne = datetime.datetime.now().strftime("%Y%m%d%H%M%S%f")
        mobile_otp = random.randint(1000, 9999)
        email_otp = random.randint(1000, 9999)
        self.message_otp(otp=mobile_otp, number=number)
        with smtplib.SMTP("smtp.gmail.com", 587) as sent_mail:
            sent_mail.starttls()
            sent_mail.login(user=my_email, password=passwords)
            response = sent_mail.sendmail(from_addr=my_email, to_addrs=email,
                               msg=f"Subject:Your OTP for verification.\n\n\n Your OTP for verification is {email_otp}. "
                                   f"Do not share with anyone")
            print(response.values())
            sent_mail.close()

        new_record = Verification(account=account, identification=newOne, mobile_OTP=mobile_otp, email_OTP=email_otp)
        with app.app_context():
            db.session.add(new_user)
            db.session.add(new_record)
            db.session.commit()
        return newOne

    def verify_user(self, data, mobile_otp, email_otp):
        """Verifies the new user"""
        with app.app_context():
            query = Verification.query.filter_by(identification=data).all()
            mobile_otp_list = [i.mobile_OTP for i in query]
            email_otp_list = [i.email_OTP for i in query]

            if int(mobile_otp) in mobile_otp_list and int(email_otp) in email_otp_list:
                return True
            else:
                return False


    def complete_account_create(self, identity,url):
        """Fills the leftover fields"""
        with app.app_context():
            query = Verification.query.filter_by(identification=identity).first()
            account_query = Bank.query.filter_by(account=query.account).first()
            print(account_query)
            email = account_query.email
            a = random.randint(1000, 9999)
            account_query.CustomerId = a
            db.session.commit()
            msg = MIMEMultipart()
            msg['Subject'] = 'Subject:Your Account Credential - Team JORGON.'
            msg.attach(MIMEText(f"Welcome to jorgon\n"
                                       f"Your Customer ID: {a}\nReset your password by clicking this link  <a href= {url}> Click here</a>", 'html'))
            with smtplib.SMTP("smtp.gmail.com", 587) as sent_mail:
                sent_mail.starttls()
                sent_mail.login(user=my_email, password=passwords)
                sent_mail.sendmail(from_addr=my_email, to_addrs=email,
                                   msg=msg.as_string())
                sent_mail.close()

    def newuser_password(self,data, passcode):
        """Sets new password for the user"""
        with app.app_context():
            query = Verification.query.filter_by(identification=data).first()
            account_query = Bank.query.filter_by(account=query.account).first()
            account_query.password = int(passcode)
            db.session.delete(query)
            db.session.commit()
            return True






