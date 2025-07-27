# Variables
PROJECT_NAME = Pinger.Monitor
PINGER_PROJECT_NAME = pinger.console
PUBLISH_DIR = ./publish
PORT = 5284
DLL_NAME = $(PROJECT_NAME).dll
PINGER_DLL_NAME = ${PINGER_PROJECT_NAME}.dll
DOTNET_VERSION= 8

.PHONY: help
help:
	@echo "Available simplified targets:"
	@echo "  check-dotnet   - Check for .net ${DOTNET_VERSION} sdk or install if not found. Do this first."
	@echo "  remove-dotnet  - Remove the .net ${DOTNET_VERSION}."
	@echo "  restore        - Restore NuGet packages"
	@echo "  build          - Build the application"
	@echo "  publish        - Publish the application to the ${PUBLISH_DIR} folder"
	@echo "  start          - Publish ${PUBLISH_DIR} folder and Run application on port $(PORT)"
	@echo "  stop           - Stop the web application"
	@echo "  run-pings      - Run test pings, use CTL + C to stop."
	@echo "  clean          - Clean publish artifacts"


.PHONY: check-dotnet
check-dotnet:
	@echo "Checking for .NET 8 SDK..."
	@if command -v dotnet &> /dev/null; then \
		if dotnet --list-sdks | grep -q "8.0."; then \
			echo ".NET 8 SDK is already installed."; \
		else \
			echo ".NET 8 SDK not found. Installing..."; \
			$(MAKE) install-dotnet; \
		fi; \
	else \
		echo "dotnet command not found. Installing .NET 8 SDK..."; \
		$(MAKE) install-dotnet; \
	fi

.PHONY: install-dotnet
install-dotnet:
	@echo "Installing .NET 8 SDK..."
	# For Ubuntu/Debian:
	sudo apt update
	sudo apt install -y dotnet-sdk-8.0

	# My Mac only
	# brew search dotnet
	# brew install --cask dotnet-sdk@${DOTNET_VERSION}
	# @echo "Successfully installed ${DOTNET_VERSION}"
	dotnet --version

.PHONY: remove-dotnet
remove-dotnet:
	# For Ubuntu/Desbian:
	sudo apt remove dotnet-sdk-8.0

	# My mac only
	# brew uninstall --cask dotnet-sdk@8

.PHONY: restore
restore:
	dotnet restore

.PHONY: build
build:
	dotnet build

.PHONY: publish
publish:
	dotnet publish -c Release -o ${PUBLISH_DIR}

.PHONY: run
run:
	@if [ ! -f "$(PUBLISH_DIR)/$(DLL_NAME)" ]; then \
		echo "Application not found. Running 'make publish' first..."; \
		$(MAKE) publish; \
	fi
	cd $(PUBLISH_DIR) && \
	ASPNETCORE_URLS="http://localhost:$(PORT)" \
	nohup dotnet $(DLL_NAME) > app.log 2>&1 &
	@echo "Application started in background on port $(PORT)"
	@echo "Check app.log for output"

.PHONY: run-pings
run-pings:
	cd ${PUBLISH_DIR} && \
	dotnet $(PINGER_DLL_NAME) -h yahoo.com,google.com -p 80 -i 5000

.PHONY: start
start: publish run

.PHONY: stop
stop:
	@pkill -f "dotnet.*$(DLL_NAME)" || echo "No running instance found"

.PHONY: clean
clean:
	sudo rm -rf ${PUBLISH_DIR}
