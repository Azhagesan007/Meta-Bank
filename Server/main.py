from flask import Flask, jsonify, request, make_response
from account import Status
from money import Money

money = Money()
status = Status()

app = Flask(__name__)


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
                                 pin=request.args.get("pin"), amount=request.args.get("amount"), customer=request.args.get("CustomerId"))
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
                "data": access, #Returns list of JSON
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
                "data": access, #Returns list of JSON
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




