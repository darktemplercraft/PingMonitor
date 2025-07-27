# Pinger Monitor

A simple host pinger console application with a web dashboard to monitor ping results in real-time.

## Technologies

- **.NET C#** - Core application logic
- **Blazor** - Web dashboard front-end
- **.NET SDK 8** - Required runtime

## Prerequisites

- .NET SDK 8.0 or higher

## Installation & Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd <repository-name>
   ```

2. **Navigate to the project folder**
   ```bash
   cd Pinger.Monitor
   ```

3. **Install dependencies and run**
   ```bash
   # Install .NET SDK if not available
   make check-dotnet
   
   # Build, publish and start the web application
   make start
   ```
   
   The application will be available at `http://localhost:5284`

## Usage

### Quick Start with Test Pings

Run simulated test pings for Yahoo and Google:
```bash
make run-pings
```
*Use `Ctrl + C` to stop the pings*

### Manual Host Pings

1. Navigate to the publish directory:
   ```bash
   cd Pinger.Monitor/publish
   ```

2. Run custom ping commands:
   ```bash
   dotnet pinger.console -h <comma-separated-domains> -p <port> -i <interval-ms>
   ```

**Parameters:**
- `-h` : Comma-separated list of domains to ping
- `-p` : Port number
- `-i` : Interval between pings in milliseconds

**Example:**
```bash
dotnet pinger.console -h google.com,yahoo.com -p 80 -i 1000
```
*Use `Ctrl + C` to stop the pings*

## Available Make Targets

View all available make commands:
```bash
make help
```

## Project Structure

```
Pinger.Monitor/
├── publish/          # Published application files
├── Makefile         # Build automation
└── ...              # Source files
```

## Features

- Real-time ping monitoring
- Web-based dashboard
- Configurable ping intervals
- Multiple host monitoring
- Console-based ping execution
