from flask import Flask, jsonify, request, make_response, render_template, url_for
from account import Status
from money import Money

money = Money()
status = Status()

app = Flask(__name__)

my_email = "azhagesan807@gmail.com"
passwords = "xyujmdlgnquguoou"

@app.route("/", methods=["GET"])
def home():
    print("Hello")
    return "Welcome to Bank"


@app.route("/login", methods=["GET"])
def login():
    try:
        access = status.authenticate(user=request.args.get('CustomerId'), pin=request.args.get('Pin'))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Wrong":
            return_data = {
                "login": False,
                "data": "Wrong pin",
                "Code": 4
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/balance", methods=["GET"])
def balance():
    try:
        access = status.check_balance(user=request.args.get("CustomerId"), pin=request.args.get("Pin"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Wrong":
            return_data = {
                "login": False,
                "data": "Wrong pin",
                "Code": 4
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/change_pin/req_otp", methods=["GET"])
def otp_req_change_pin():
    try:
        access = status.otp_req_pin(user=request.args.get("CustomerId"), pin=request.args.get("Pin"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Wrong":
            return_data = {
                "login": False,
                "data": "Wrong pin",
                "Code": 4
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/sendmoney", methods=["GET"])
def send():
    try:
        access = money.send_money(receiver_account=request.args.get("ReceiverAccount"),
                                  pin=request.args.get("pin"), amount=request.args.get("amount"),
                                  customer=request.args.get("CustomerId"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Wrongs":
            return_data = {
                "login": False,
                "data": "No Receiver with this account no",
                "Code": 3
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/change_pin")
def verify_change_pin():
    try:
        access = status.pin_change(otp=request.args.get("OTP"),
                                   identity=request.args.get("ID"),
                                   new_pin=request.args.get("PIN")
                                   )
        if access == "Wrong":
            return_data = {
                "Pin change": False,
                "data": "Wrong OTP",
                "Code": 3
            }
            return make_response(jsonify(return_data), 400)

        else:
            return_data = {
                "Pin change": True,
                "data": access,
                "Code": 4
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "Pin Change": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/withdraw")
def withdraw():
    try:
        access = money.withdraw_money(userid=request.args.get("CustomerId"), amount=request.args.get("Amount"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,  # Returns list of JSON
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/deposit")
def deposit():
    try:
        access = money.deposit_money(userid=request.args.get("CustomerId"), amount=request.args.get("Amount"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,  # Returns list of JSON
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/statement")
def get_trans():
    try:
        access = money.get_trans(account_t=request.args.get("CustomerId"), pin=request.args.get("Pin"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Insufficient balance":
            return_data = {
                "login": False,
                "data": access,
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/cash", methods=["GET"])
def cash():
    try:
        access = status.check_cash(user=request.args.get("CustomerId"), pin=request.args.get("Pin"))
        if access is None:
            return_data = {
                "login": False,
                "data": "No Account Found",
                "Code": 1
            }
            return make_response(jsonify(return_data), 200)

        elif access == "Wrong":
            return_data = {
                "login": False,
                "data": "Wrong pin",
                "Code": 4
            }
            return make_response(jsonify(return_data), 200)
        else:
            return_data = {
                "login": True,
                "data": access,
                "Code": 2
            }
            return make_response(jsonify(return_data), 200)

    except KeyError:
        return_data = {
            "login": False,
            "data": "Please give valid info",
            "Code": 400
        }
        return make_response(jsonify(return_data), 400)


@app.route("/newuser/create")
def newuser():
    return render_template("new_user.html")


@app.route("/newuser/verify", methods=["POST"])
def verifyuser():
    data = request.form.get("date").split("-")
    date = ""
    for i in data:
        date = i + '/' + date
    date = date[:len(date) - 1]
    print(date)
    email = request.form.get("email")
    print(email)
    data = status.newUser(name=request.form.get("name"), number=request.form.get("number"),
                          email=email,
                          dob=date, father=request.form.get("father"))
    return render_template("verify.html", data=data, results="")

@app.route("/newuser/verify/<string:data>", methods=["POST"])
def check_verification(data):
    email_otp = request.form.get("email")
    mobile_otp = request.form.get("mobile")
    result = status.verify_user(data=data, email_otp=email_otp,mobile_otp=mobile_otp)
    if result:
        urls = "http://127.0.0.1:5000/newuser/verify/createpassword/"+data
        status.complete_account_create(data, url=urls)
        return "<h1>Created account</h1>"
    else:
        return render_template("verify.html", data=data, results="Incorrect OTP")


@app.route("/newuser/verify/createpassword/<string:data>", methods=["POST","GET"])
def create_password(data):
    if request.method == "GET":
        return render_template("password.html", data=data)
    else:
        passcode = request.form.get("password")
        next = status.newuser_password(data=data, passcode=passcode)
        if next:
            return "<h1>Password set succesfull</h1>"
        else:
            return"None"

