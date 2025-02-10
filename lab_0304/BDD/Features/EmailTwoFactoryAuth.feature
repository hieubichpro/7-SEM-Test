Feature: EmailTwoFactoryAuth

Scenario: SignIn
	Given user enters the login information
	When user sends a login request
	Then system should send the code to the email

	Given user enters the received code
	When user sends a confirmation code
	Then system confirms the email

Scenario: ChangePassword
	Given user enters new password
	When user send a change password request
	Then system should send the code to emaill

	Given user enters the received codee
	When user sends a confirmation codee
	Then system confirms the new password