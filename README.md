# BookStoreTester

[![Watch the video](https://img.youtube.com/vi/j0BNrRH84Cw/0.jpg)](https://youtu.be/j0BNrRH84Cw)

**BookStoreTester** is a web application designed for testing bookstore applications by generating fake book data. The app allows you to customize parameters such as language, region, average likes, and reviews per book. It supports infinite scrolling and exporting data to CSV.

## Features

- **Language and Region Selection**: Supports at least 3 different languages/regions, e.g., English (US), Russian (RU), French (FR).
- **Random Data Generation**: Generates random records for books, including ISBN, title, authors, and publishers.
- **Likes and Reviews Adjustment**: Allows users to set the average number of likes and reviews per book using sliders and input fields.
- **Infinite Scrolling**: Dynamically adds new records as the user scrolls down the table.
- **CSV Export**: Allows the user to export the current data to a CSV file using a proper CSV formatter.
- **Gallery View**: A toggle between table view and gallery view for displaying book information.

## How Data Generation Works

The app generates random book data for each page based on the initial seed value and selected region. When the user changes the initial value, the generated data changes accordingly, ensuring that each page produces unique records. The seed value is combined with the page number to ensure consistent data across sessions. This means if the user sets the same initial value tomorrow, they will get the same data across all pages.

## Demo

In the app, users can:
- Select a language and region to generate data.
- Adjust the average number of likes and reviews using a slider.
- View the books in either table format or as a gallery.
- Export the displayed data to a CSV file.

## Example Use Case

1. Open the app, where the default language is **English (US)**.
2. Change the region to **French (FR)** — the book titles and authors will change accordingly.
3. Adjust the average number of likes and reviews using the sliders and watch the data change.
4. As you scroll down the table, new books will be added automatically.
5. Export the current data displayed in the table to a CSV file.

## Technologies Used

- **Frontend**: Blazor (Server)
- **Backend**: ASP.NET Core
- **Fake Data Generation**: Bogus, some image parsing and custom functions
- **CSV Export**: CsvExport package
