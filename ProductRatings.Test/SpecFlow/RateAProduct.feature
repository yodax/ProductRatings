Feature: Rate a product

Scenario: Give a product a rating

Given a product
When I rate the product 5 stars
Then the product should have an average 5 star rating

Scenario: Multiple ratings on a product should be averaged

Given a product
When I rate the product 5 stars
And I rate the product 1 stars
Then the product should have an average 3 star rating

Scenario: Each product should have a own rating

Given a product called "Toothbrush"
And a product called "Pencil"
When I rate a "Pencil" 4 stars
And I rate a "Toothbrush" 3 stars
Then a product called "Pencil" has an average rating of 4 stars
And a product called "Toothbrush" has an average rating of 3 stars

Scenario: Default rating

Given a product
When I dont rate the product
Then the product should not have a rating

Scenario Outline: Only valid ratings should be accepted

Given a product
When I give the product an invalid rating of <rating>
Then the product should not have a rating

Examples: 
| rating |
| -1     |
| 0      |
| 6      |

Scenario Outline: Valid ratings

Given a product
When I rate the product <stars> stars
Then the product should have an average <stars> star rating

Examples: 
| stars |
| 1     |
| 2     |
| 3     |
| 4     |
| 5     |