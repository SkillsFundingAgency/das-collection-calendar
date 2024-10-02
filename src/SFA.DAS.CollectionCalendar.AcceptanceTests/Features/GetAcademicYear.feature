@api
@database
Feature: GetAcademicYear

Scenario: Get an Academic Year by Year
	Given an academic year is requested for a valid year
	Then the academic year details are returned
