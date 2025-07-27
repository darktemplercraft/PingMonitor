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
   git clone https://github.com/darktemplercraft/PingMonitor
   cd PingMonitor
   ```

2. **Navigate to the project folder**
   ```bash
   cd Pinger.Monitor
   ```

3. **Install dependencies and run**
   ```bash
   # Install .NET SDK if not available
   make check-dotnet

   # Install Nuget Packages
   make restore
   
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

## Nat's Notes

### Technology Choices

**Backend Framework**: I chose .NET for this project as it's the technology I'm most familiar with that provides excellent cross-platform support for Linux environments. This ensures reliable deployment across different operating systems.

**Frontend Framework**: Blazor was selected for the web dashboard to minimize complexity and streamline deployment. The server-side rendering approach eliminates the need for separate API endpoints and simplifies the overall architecture. For a production project, I would likely opt for React to provide more flexibility and a richer ecosystem of components.

**Data Storage**: The application uses in-memory caching for data storage to enable fast retrieval and avoid file system complications during environment deployments. This approach prioritizes simplicity and deployment reliability. In a production environment, this should be replaced with a proper database solution (SQL Server, PostgreSQL, etc.) for data persistence and scalability.

**Chart Display**: The line chart currently displays ping results for only the last 3 minutes to focus on the most recent network performance data. This provides a clear view of current connectivity status without overwhelming the interface.

### Future Enhancements

- **Flexible Time Ranges**: Add user-configurable time range options (last 30 minutes, 1 hour, 24 hours, etc.) for the ping result charts
- **Database Integration**: Implement proper database storage for historical data persistence
- **Alert System**: Add notifications for ping failures or high latency thresholds
- **Export Functionality**: Enable data export for reporting and analysis
- **Advanced Filtering**: Implement filtering by host, time range, and status
