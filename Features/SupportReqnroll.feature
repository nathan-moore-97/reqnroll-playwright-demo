Feature: SupportReqnroll

This feature will check the subscription packages

Scenario: Subscription Packages
	Given The Reqnroll page is loaded
	When The support button is clicked
	Then The following Support Page texts are visible
	| Key   | Value        |
	| Tier1 | Community    |
	| Tier2 | Professional |
	| Tier3 | Enterprise   |

Scenario: Quickstart Loads
	Given The Reqnroll page is loaded
	When The quickstart button is clicked
	Then a new tab should be opened to "https://docs.reqnroll.net/latest/quickstart/index.html"

Scenario: The news feed loads
	Given The Reqnroll page is loaded
	Then the news feed has items

Scenario: Learn More Headings
	Given The Reqnroll page is loaded
	When The Discover More button is clicked
	Then The following about page texts are visible
	| Key | Value                |
	| 0   | Relation to SpecFlow |
	| 1   | Relation to Cucumber |
	| 2   | Sustainability       |
	| 3   | Contact              |