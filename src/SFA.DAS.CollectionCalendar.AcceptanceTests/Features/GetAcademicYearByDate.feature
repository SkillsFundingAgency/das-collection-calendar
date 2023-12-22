@api
@database
Feature: GetAcademicYearByDate

Scenario: Get an Academic Year by Date
	Given an academic year is requested for a valid date
	Then the academic year details are returned
