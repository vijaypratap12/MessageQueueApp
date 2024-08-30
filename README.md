## Running the Application
1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Set `MessageQueueApp` as the startup project.
4. Run the project. The console output will show the processing results and the logged messages.

## Running the Tests
1. Right-click on the `MessageQueueApp.Tests` project in Visual Studio.
2. Select **Run Tests**. All tests should pass successfully.

## Structure
- `MessageQueueApp.Business`: Contains the business logic for message handling.
- `MessageQueueApp`: The main console application that uses the business logic.
- `MessageQueueApp.Tests`: Unit tests for the business logic using xUnit.
