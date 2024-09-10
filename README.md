# GeoLocation Utility

This utility fetches geographic data like latitude, longitude, place name, and state using city/state names or ZIP codes. It leverages the OpenWeatherMap Geocoding API to provide accurate location data.

## Table of Contents
- [Overview](#overview)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Testing](#testing)
- [API Details](#api-details)

## Overview

The GeoLocation Utility lets you input city/state combinations or ZIP codes to retrieve detailed geographical information. You can submit multiple locations at once, and the tool will return the corresponding latitude, longitude, and more.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- A reliable internet connection to access the OpenWeatherMap API.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/geolocation-fetcher.git
   cd geolocation-fetcher
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Usage

To run the utility, simply use the following command:

```bash
dotnet run -- [location]
```

**Examples**:
- For a city and state:
  ```bash
  dotnet run -- "Madison, WI"
  ```
- For a ZIP code:
  ```bash
  dotnet run -- "10001"
  ```
- To provide multiple locations:
  ```bash
  dotnet run -- "Madison, WI" "10001" "Los Angeles, CA" "Chicago, IL"
  ```

## Testing

To run the tests, use this command:

```bash
dotnet test
```

## API Details

This utility uses the OpenWeatherMap Geocoding API to retrieve location data. You’ll need an API key to access this service. While a sample API key is included, it may have limited access, so it's recommended to get your own from [OpenWeatherMap](https://openweathermap.org/).
