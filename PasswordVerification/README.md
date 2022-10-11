# Password Validation

Simple extensible solution for password validation.

Rules:
1.	- password should be larger than 8 chars
2.	- password should not be null
3.	- password should have one uppercase letter at least
4.	- password should have one lowercase letter at least
5.	- password should have one number at least
RuleSet:
1.	Add feature: Password is OK if at least three of the previous conditions is true
2.	Add feature: password is never OK if item 1.d is not true.

# Solution
The solution is developed based on .NET 6. It is designed using C#.

The projects are divided into Core, Rule, RuleSet, Engine and App(Application).

## Framework details
.NET 6

## Build
To build the project one can build the "Password.App" project and run it from Visual Studio.

It can also be done from console by running the following command from the solution folder

First restore the dependencies which is needed for building the solution

`dotnet restore`

and build it using the following command

`dotnet build`

## Test
Once the solution is built you can run the following command to run the tests

`dotnet test`

## Run
To run the project use the following command from command line or set "Password.App" as the startup project and run it from Visual Studio

`dotnet run --project Password.App`

# Input
The default implementation takes input from console and reads the password for verification.

Note: For simplicity, the password will be visible during entry however we can mask it.

# Output
Sample Input/Output E.g.

`$ dotnet run --project Password.App`
`Please enter your password:`
`abcd`

`Verification Failed`

`Rules:`
`1. Password should be more than 8 characters in length. (failed)`
`2. Password should be provided. (pass)`
`3. Password should contain at least one digit. (failed)`
`4. Password should contain at least one lower case. (pass)`
`5. Password should contain at least one lower case. (failed)`

`RuleSets:`
`1. At least 3 password rule(s) should be satisfied. (failed)`
`2. All mandatory password rules should be satisfied, mandatory rules are 1,2,3,4,5 (failed)`

`Please enter your password:`
`abcded1`

`Verification Failed`

`Rules:`
`1. Password should be more than 8 characters in length. (failed)`
`2. Password should be provided. (pass)`
`3. Password should contain at least one digit. (pass)`
`4. Password should contain at least one lower case. (pass)`
`5. Password should contain at least one lower case. (failed)`

`RuleSets:`
`1. At least 3 password rule(s) should be satisfied. (pass)`
`2. All mandatory password rules should be satisfied, mandatory rules are 1,2,3,4,5 (failed)`

`Please enter your password:`
`abcded989B`

`OK`

`Rules:`
`1. Password should be more than 8 characters in length. (pass)`
`2. Password should be provided. (pass)`
`3. Password should contain at least one digit. (pass)`
`4. Password should contain at least one lower case. (pass)`
`5. Password should contain at least one lower case. (pass)`

`RuleSets:`
`1. At least 3 password rule(s) should be satisfied. (pass)`
`2. All mandatory password rules should be satisfied, mandatory rules are 1,2,3,4,5 (pass)`

