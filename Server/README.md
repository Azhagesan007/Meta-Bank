Welcome to the Bank API documentation. This API allows you to perform various banking operations such as login, balance inquiry, fund transfer, PIN change, cash withdrawal, cash deposit, transaction statement retrieval, and cash check. The API is built using Flask, a Python web framework.
Base URL: http://your-domain.com/

Endpoints:

Home

URL: /
Method: GET
Description: Returns a welcome message.
Response: "Welcome to Bank"
Login

URL: /login
Method: GET
Description: Authenticates a user and returns login status and account information.
Parameters:
CustomerId: The customer ID (query parameter)
Pin: The PIN for authentication (query parameter)
Response:
login: Login status (True/False)
data: Account information or error message
Code: Response code indicating the result
Balance

URL: /balance
Method: GET
Description: Retrieves the account balance for the authenticated user.
Parameters:
CustomerId: The customer ID (query parameter)
Pin: The PIN for authentication (query parameter)
Response:
login: Login status (True/False)
data: Account balance or error message
Code: Response code indicating the result
Request OTP for PIN Change

URL: /change_pin/req_otp
Method: GET
Description: Requests an OTP (One-Time Password) for changing the PIN.
Parameters:
CustomerId: The customer ID (query parameter)
Pin: The PIN for authentication (query parameter)
Response:
login: Login status (True/False)
data: OTP request status or error message
Code: Response code indicating the result
Send Money

URL: /sendmoney
Method: GET
Description: Sends money from the authenticated user's account to another account.
Parameters:
ReceiverAccount: The recipient's account number (query parameter)
pin: The PIN for authentication (query parameter)
amount: The amount to send (query parameter)
CustomerId: The customer ID (query parameter)
Response:
login: Login status (True/False)
data: Transaction status or error message
Code: Response code indicating the result
Verify PIN Change

URL: /change_pin
Method: GET
Description: Verifies the OTP and changes the PIN for the authenticated user.
Parameters:
OTP: The One-Time Password received for PIN change (query parameter)
ID: The identification for the OTP (query parameter)
PIN: The new PIN to set (query parameter)
Response:
Pin change: PIN change status (True/False)
data: PIN change status or error message
Code: Response code indicating the result
Withdraw Money

URL: /withdraw
Method: GET
Description: Withdraws money from the authenticated user's account.
Parameters:
CustomerId: The customer ID (query parameter)
Amount: The amount to withdraw (query parameter)
Response:
login: Login status (True/False)
data: Withdrawal transaction details or error message
Code: Response code indicating the result
Deposit Money

URL: /deposit
Method: GET
Description: Deposits
