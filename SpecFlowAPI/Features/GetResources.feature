Feature: Get Resources


@GetResources
Scenario: Get a list of resources
	When I make the API call to get list of resources
	Then Status code is 200
	And The first page of resources is retrieve


@GetResource
Scenario Outline: Get as single resource
	When I make the API call to retrieve a resource with "<id>"
	Then The response status code is 200
	And the resource with "<id>" "<name>" "<year>" is retrieved


	Examples: 
	| id | name         | year |
	| 2  | fuchsia rose | 2001 |
	| 4  | aqua sky     | 2003 |


@GetResourceNotFound
Scenario Outline: Get a single resource (not found)
	When I make the API call to retrieve a resource with "<id>"
	Then The response status code is 404

	Examples: 
	| id |
	| 23 |
	| 45 |