Gamedalf
========

Project for Systems Development Methodologies class at UFSCar.

## License

This project is avaiable under the MIT license. You can check the license file
[here](https://github.com/lucasdavid/Gamedalf/blob/master/LICENSE).
Additional information may be found in the
[Open Source Initiative](http://opensource.org/licenses/MIT)'s website.

## Compiling

The database is being generated on-the-fly.
However, for obvious reasons, the `.mdf` file is not being tracked by GIT.
In order to update the database, you have to run the command `Update-Database -ProjectName Gamedalf.Core`
in Visual Studio's `Package Manager Console`.

Dependency packages can be updated by:
* Clicking with the right button the item `Solution 'Gamedalf'` in the Solution Explorer
* Selecting `Manage NuGet Packages for Solution...`
* Navigate through the tabs `Update` > `All`
* Finally, clicking on the button `Update all`.

## Project planning

Use the [milestones](https://github.com/lucasdavid/Gamedalf/milestones) to see all the planning that the group is doing.

## Test

All tests were kept in a single Class Library called [Gamedalf.Tests](https://github.com/lucasdavid/Gamedalf/tree/master/Gamedalf.Tests). They all follow the <a href="http://c2.com/cgi/wiki?ArrangeActAssert">Arrange-Act-Assert</a> pattern. Units being tested are the [Controllers](https://github.com/lucasdavid/Gamedalf/tree/master/Gamedalf.Tests/Controllers) and the [Services](https://github.com/lucasdavid/Gamedalf/tree/master/Gamedalf.Tests/Services). The library <a href="https://github.com/Moq/moq4/">Moq</a> was used for mocking.
