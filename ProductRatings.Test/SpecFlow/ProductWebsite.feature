Feature: Product Website
	
Scenario: Show top rated products

Given a list of 5 products rated 1 stars
And a list of 5 products rated 2 stars
And a list of 5 products rated 3 stars
And a list of 5 products rated 4 stars
And a list of 5 products rated 5 stars
When I request an overview
Then the overview should show the first 10 products ordered by rating

Scenario: Populate database with products

Given a catalog
When I populate the database
Then there should be products present

Scenario: Database should be reset

Given a catalog
And an existing product called "Pencil"
When I populate the database
Then it should not contain a "Pencil"

Scenario: Show all products

Given a list of 5 products rated 1 stars
And a list of 5 products rated 2 stars
And a list of 5 products rated 3 stars
When I request an overview
Then the overview should show all products ordered by name
