Feature: Get Users

@GetUsers
Scenario Outline: Get a list of users
	When I make the API call to get the user page number "<pageNumber>"
	Then The request should be successful with status code 200
	And The user page number "<pageNumber>" is retrieved
	And There are 6 users per page

	Examples:
	| pageNumber |
	| 1			 |
	| 2			 |

@GetUser
Scenario Outline: Get a single user
	When I make the Api call to get a user "<userId>"
	Then The response status code should be 200
	And The user id "<userId>" is retrieved

	Examples: 
	| userId |
	| 1      |
	| 4      |
	| 10     |

@GetUserNotFound
Scenario Outline: Get a single user (not found)
	When I make the Api call to get a user "<userId>"
	Then The response status code should be 404

	Examples: 
	| userId |
	| 23     |
	| 45     |