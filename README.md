# helsinki-city-bike-app

This is an excercise for Solita Dev Academy 2023
<https://github.com/solita/dev-academy-2023-exercise>

## Prerequisites

- docker (v 20.10.21)
- docker-compose (v 1.29.2)
- CSV files from HSL
  - <https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv>
  - <https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv>
  - <https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv>
  - <https://opendata.arcgis.com/datasets/726277c507ef4914b0aec3cbcfcbfafc_0.csv>

The project was developed on Ubuntu 23.04 and I tested it on Fedora 38. Don't know if Windows will run into problems, but I would bet it runs on any machine that has a relatively recent version of docker and docker-compose.

## How to run

- Copy the .csv files into folder `bike-webapi` (important: don't rename any of the .csv files!)
- In the project root run `docker-compose build`
- Then `docker-compose up`

The API will start reading the CSV files and inserts them in to the database on first run. This will take around 5-10 minutes, but on a slower machine might take a bit longer. After that the frontend should be running and serving on <http://localhost:5173> and backend on <http://localhost:5000>

## Description

This is a web application that displays data from HSL City Bikes. It lists bike stations and journey data for the months of May, June and July of 2021.

The web page has views for journey listing, station listing and details of a station. Journeys can be sorted by column, searched and filtered by month. Stations can be searched in the listing. Single station view has additional details that can be filtered by month.

### Backend endpoints

Serves from <http://localhost:5000>

#### List stations

`/api/stations`

Query params

- `page` page number
- `pageSize` page size
- `search` search stations

Returns a total count of stations (with the used query params, before pagination) and a pageful of stations in a list.

#### List journeys

`/api/journeys`

Query params

- `page` page number
- `pageSize` page size
- `search` search journeys
- `departureStationId` filter by departure station id
- `returnStationId` filter by return station id
- `month` filter by month (number)
  `sortBy` sort by column.

The syntax for sorting is `<column name>` appended by either Ascending or Descending for example `departureAscending`

Returns a total count of journeys (with the used query params, before pagination) and a pageful of journeys in a list, sorted by the orderBy param.

#### Single station

`/api/stations/<id>`

`id` of a station

Returns basic information of a station.

#### Single station details

`/api/stations/<id>/details`

`id` of a station

Query params

`month` filters details by month

Returns computed details of a station:

- Total departures
- Total returns
- Average distance of a journey starting from the station
- Average distance of a journey ending at the station
- Top 5 destinations for journeys starting from the station
- Top 5 origins for journeys ending at the station

## Technologies used

- C#
- JavaScript
- dotnet CLI
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- VITE
- React
- Material UI
- Docker
- Docker Compose
- Git
- Visual Studio Code
- Postman

At the beginning of the project I was considering using NextJS instead of ASP.NET Core and React. I ended up using ASP.NET Core with C# for the backend as this is something I have the most experience with. I chose to run everything on docker and docker-compose to make it smooth as possible to run on another machine. No need to configure anything, just building and running docker.

MS SQL Server is something I have been using together with Entity Framework Core. Familiarity is golden.

Vite was chosen to scaffold and run the frontend as it is noticeably faster and better than create-react-app. Material UI was chosen as the component library as I am very familiar with that and it makes development of a clean and nice website very fast and easy.

## TODO

- Unit testing
- Views to add journeys and stations
- Display station location on a map
- Ordering stations by column
- Filter journeys by station id
