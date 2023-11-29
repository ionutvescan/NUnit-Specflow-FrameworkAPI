Feature: Registration


@SuccessfulRegistration
Scenario: The user is able to register
	Given I populate the API call with email address and password
	When I make the API call to register
	Then the API call is successful with status 200


@UnsuccessfulRegistration
Scenario: Fail to register (no password provided)
	Given I populate the API call with email address
	When I make the API call to register
	Then the API call is unsuccessful with status code 400
	And the error is "Missing password"
