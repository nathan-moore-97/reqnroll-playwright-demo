Feature: SupportReqnroll

This feature will check the subscription packages

Scenario: SubscriptionPackages
	Given The Reqnroll page is loaded
	When The support button is clicked
	Then The following textx are visible
	| Key   | Value        |
	| Tier1 | Community    |
	| Tier2 | Professional |
	| Tier3 | Enterprise   |


Scenario: QuickstartLoads
	Given The Reqnroll page is loaded
	When The quickstart button is clicked
	Then a new tab should be opened to "https://docs.reqnroll.net/latest/quickstart/index.html"