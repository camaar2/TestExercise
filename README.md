# Test Assignment 

Hello! This web-application is for managing parcels and letters in a post office. I made this project using Angular for front-end and InMemoryDatabase for the database. The code is made in 2 days. I added Postman ApiTests to check the Api Controllers.

# Files


In `src/app`  folder i have three main components:  `home`,  `app` ja  `shipments`.
Home component is made to show the home page that includes the banner and shipment information, including the bags information, when you click on the shipment. `App` holds information for the logo and header. `Shipment` component holds the main info for the shipments, that are exported into the `Home` component.

In BackEnd I created models and controllers for bag, shipment and parcel. I also added DataContext where i configured the entities and also used data seeding.

## To open the project

To install and run this application locally on your machine, follow these steps:

1. Clone the repository to your local machine: `git clone [https://github.com/camaar2/TestExercise.git]`
2. Navigate to the project directory: `cd Frontend`
3. Install dependencies: `npm install` (this requires downloading Node.js node package manager (npm))
4. Start the backend by running the project.
5. Start the development server: `ng serve` (this requires downloading Angular CLI (`npm install -g @angular/cli`))
6. Open your web browser and visit `http://localhost:4200` to view the application.


## Usage

Upon launching the application, you can click on the "Shipment info" box, which will toggle and display more information about Shipments and Bags. Backend includes entity validations. I utilized Swagger to gain a better overview of the Backend and to test the functionality of the API controllers.
To use the ApiTests, I run my project and write in my terminal `newman run apitests/testtoo.postman_collection.json -e apitests/testtoo.postman_environment.json`
