from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from sqlalchemy import or_
from account import Status
import datetime

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = "sqlite:///jorgon.db"
db = SQLAlchemy(app)



class Transaction(db.Model):
    id = db.Column(db.Integer, unique=True, nullable=False, primary_key=True)
    time = db.Column(db.Integer, unique=False, nullable=False)
    amount = db.Column(db.Float, unique=False, nullable=False)
    sender_id = db.Column(db.Integer, unique=False, nullable=False)
    receiver_id = db.Column(db.Integer, unique=False, nullable=False)
    sender_balance = db.Column(db.Integer, unique=False, nullable=True)
    receiver_balance = db.Column(db.Integer, unique=False, nullable=True)


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


class Money:
    """Maintains the transaction in bank"""
    def __init__(self):
        self.status = Status()
        # self.banks = Bank()
        self.none = None

    def send_money(self, customer, receiver_account, amount, pin):
        """Controls the transaction"""
        status = self.status.authenticate(pin=pin, user=customer)
        with app.app_context():
            print(receiver_account)
            receiv = Bank.query.filter_by(account=int(receiver_account)).first()
            print(receiv.name)
            print(type(receiv))
        if status == "Wrong" or status is None or receiv is None:
            if receiv is None:
                status = "Wrongs"
            return status
        else:
            if float(amount) <= receiv.balance:
                with app.app_context():
                    amount = float(amount)
                    get_sender_account = Bank.query.filter_by(CustomerId=customer).first()
                    get_receiver_account = Bank.query.filter_by(account=int(receiver_account)).first()
                    get_sender_account.balance -= amount
                    print("Done1")
                    t_amount = amount * -1
                    get_receiver_account.balance += amount
                    self.status.message_amount(balance=get_sender_account.balance, amount=t_amount,
                                                number=get_sender_account.mobile)

                    self.status.message_amount(balance=get_receiver_account.balance, amount=amount,
                                                number=get_receiver_account.mobile)
                    trans = Transaction(time=datetime.datetime.now().strftime("%Y%m%d%H%M%S"), amount=amount,
                                        sender_id=get_sender_account.account, receiver_id=int(receiver_account),
                                        sender_balance=get_sender_account.balance,
                                        receiver_balance=get_receiver_account.balance)
                    print("Done2")
                    db.session.add(trans)
                    db.session.commit()
                    print("Done3")
                    return f"{amount} transfer successful"
            else:
                return "Insufficient balance"

    def get_trans(self, account_t, pin):
        with app.app_context():
            users = Bank.query.filter_by(CustomerId=account_t).first()
            cus = Transaction.query.filter(or_(Transaction.sender_id == users.account, Transaction.receiver_id == users.account)).all()
        new_t = []
        for i in cus:
            ds = i.time
            sec = ds % 100
            mini = int(((ds % 10000) - sec) / 100)
            hr = int(((ds % 1000000) - mini - sec) / 10000)
            day = int(((ds % 100000000) - mini - sec - hr) / 1000000)
            month = int(((ds % 10000000000) - mini - sec - hr - day) / 100000000)
            year = int((ds - mini - sec - hr - day - month) / 10000000000)
            if i.sender_id == users.account:
                get_dict = {
                    "time": f"Time: {hr}:{mini}:{sec} in date: {day}/{month}/{year}",
                    "amount":  i.amount * -1,
                    "type": "Debit"
                    }
            elif i.receiver_id == users.account:
                get_dict = {
                    "time": f"Time: {hr}:{mini}:{sec} in date: {day}/{month}/{year}",
                    "amount": i.amount,
                    "type": "credit"
                    }
            new_t.append(get_dict)
        return new_t


    def withdraw_money(self, userid, amount):
        """Performs the withdraw operation"""
        with app.app_context():
            get_data = Bank.query.filter_by(CustomerId=userid).first()

        if get_data is None:
            return None
        elif get_data.balance <= int(amount):
            return "Insufficient balance"
        else:

            t_amount = int(amount) * -1
            with app.app_context():
                get_data = Bank.query.filter_by(CustomerId=userid).first()
                get_data.balance -= int(amount)
                get_data.cash += int(amount)
                trans = Transaction(time=datetime.datetime.now().strftime("%Y%m%d%H%M%S"), amount=t_amount,
                                        sender_id=get_data.account, receiver_id=0,
                                        sender_balance=get_data.balance)
                db.session.add(trans)
                db.session.commit()

            return "Successful"


    def deposit_money(self, userid, amount):
        """Performs the deposit operation"""
        with app.app_context():
            get_data = Bank.query.filter_by(CustomerId=userid).first()
            print(type(get_data.cash))
            print(amount)
        if get_data is None:
            return None
        elif get_data.cash <= int(amount):
            return "Insufficient balance"
        else:

            with app.app_context():
                get_data = Bank.query.filter_by(CustomerId=userid).first()
                get_data.balance += int(amount)
                get_data.cash -= int(amount)
                trans = Transaction(time=datetime.datetime.now().strftime("%Y%m%d%H%M%S"), amount=amount,
                                        sender_id=0, receiver_id=get_data.account,
                                        sender_balance=get_data.balance)
                db.session.add(trans)
                db.session.commit()

            return "Successful"



