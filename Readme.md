# GeoLocation Utility

This utility retrieves geographical location data, including latitude, longitude, place name, and state, based on city/state names or ZIP codes using the OpenWeatherMap Geocoding API.

## Table of Contents
- [Overview](#overview)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Testing](#testing)
- [API Details](#api-details)

## Overview

The GeoLocation Utility allows users to input a list of locations in the form of city/state combinations or ZIP codes and returns the corresponding geographical data. It can handle multiple inputs at once and provides informative messages based on the results.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- An active internet connection to access the OpenWeatherMap API.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/geolocation-fetcher.git
   cd geolocation-fetcher
   
2.Restore the dependencies:
	dotnet restore
	
### Usage

- To run the utility, use the following command in the terminal:
	dotnet run -- [location]
	
	Examples
		-For a city and state:
			dotnet run -- "Madison, WI"
		-For a ZIP code:
			dotnet run -- "10001"
		-To input multiple locations:
			dotnet run -- "Madison, WI" "10001" "Los Angeles, CA" "Chicago, IL"

### Testing

-To run the tests, execute the following command in the terminal:
	dotnet test

### API Details

-This utility uses the OpenWeatherMap Geocoding API. 
	-An API key is required for accessing the OpenWeatherMap API. The utility is configured with a sample API key, which may have limitations.


