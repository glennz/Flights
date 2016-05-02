App to manage Flights of Terminal gate
Author: Glenn Zheng
Date: 03 May 2016

Technologies:
WebApi 2.2
.Net 4.5 as required
visual studio 2013
Packages: AutoMap, Autofac(for dependency injection)
Unit testing: MSTest, Moq

Backend architechture:

UI (Angularjs) <- WebApi <- Service layer / Dto <- Repo / Model

Angularjs 1.4.8 components driven.  Bootstrap and UI-Bootstrap. Font-awsome
Angularjs sit on the top of MVC for eazy launch only and review.


*********Instruction to run the project*********

1 Download the project from github by clicking the download button and get a zip file

2 Extract the zip file

3 Double click the .sln file to open the project in Visual studio 2013.

4 Build -> Build Soultion

5 Hightlight Solution 'App', click Run



Angular directory:
Scripts/app/flightInfo/components/
Scripts/app/flightInfo/services/

Things not include:
1 validation at UI form.  Add flight, update flight and assign flight to another gate
2 server side logging is not inlcuded
3 UI datetime textbox yet to use datetime picker.
4 temporary break cache in angularjs service. This is for IE testing. Chrome works fine.
5 data is store in inline memory, not store in database as requirement.
6 no fancy UI design due to time limit
7 no UI automation test such as selenium due to time limit
8 Unit test code coverage is not 100%. It is just for demo of skill.

