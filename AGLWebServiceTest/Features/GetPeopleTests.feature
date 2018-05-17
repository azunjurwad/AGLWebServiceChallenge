Feature: GetPeopleTests

Scenario: Validate all cats are in alphabetical order under heading of gender
	
	Given I get all owners and pets from the web service
	| ApiUrl                                                  |
	| http://agl-developer-test.azurewebsites.net/people.json |
	
	When I sort sort owners alphabetically by pets name
	
	Then I should get '2' male owners
		And I should get '3' female owners
		And I should get 'Bob' and 'Fred' as first and second male owners respectively
		And I shoudl get feamale owners in expected order
		| owner    | counter |
		| Jennifer | 0       |
		| Alice    | 1       |
		| Samantha | 2       |
