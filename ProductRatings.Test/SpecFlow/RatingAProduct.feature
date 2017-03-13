Feature: Rating a product

Scenario: Rate a product

Given a product
When I rate the product 5 stars
Then the average product rating should be 5 stars

Scenario: Ratings on a product should be averaged

Given a product
When I rate the product 5 stars
And I rate the product 1 stars
Then the average product rating should be 3 stars

Scenario: Multiple products should have their own ratings

Given a product called "Toothbrush"
And a product called "Pencil"
When I rate "Toothbrush" 3 stars
And I rate "Pencil" 5 stars
Then the average rating for "Toothbrush" should be 3 stars
And the average rating for "Pencil" should be 5 stars