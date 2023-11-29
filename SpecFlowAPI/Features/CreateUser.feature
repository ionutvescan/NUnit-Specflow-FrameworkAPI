Feature: Create User
	
@CreateUser
Scenario Outline: Create a new user
	Given I populate the API call with "<userName>" and "<userJob>"
	When I make the API call to create a new user
	Then the call is successful with status 201
	And the user profile with "<userName>" and "<userJob>" is created

	Examples: 
	| userName | userJob  |
	| John     | QA       |
	| Michael  | Developer|

@UpdateUser
Scenario Outline: Update user created
	Given The user is created with "<userName>" and "<userJob>"
	When I make the API call to update "<userName>" user's job with "<newJob>"
	Then the call is successful with status 200
	And the "<userName>" user's job is update with "<newJob>"

	Examples: 
	| userName | userJob   | newJob |
	| John     | QA        | BA     |
	| Michael  | Developer | PO     |


@DeleteUser
Scenario Outline: Delete user created
	Given The user is created with "<userName>" and "<userJob>"
	When I make the API call to delete user
	Then the call is successful with status 204

	Examples: 
	| userName | userJob  |
	| John     | QA       |
	| Michael  | Developer|