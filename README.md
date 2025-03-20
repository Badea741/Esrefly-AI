# Esrefly: AI-Powered Personal Finance Application

Esrefly is a personal finance application that leverages AI as a personal finance expert.

## AI Model Selection
We chose **llama3.2:1b** as our model due to its small size and ability to run on nearly any modern machine.

## Running the AI Agent Locally
Follow these steps to set up and run the AI agent on your local machine.

### Step 1: Start Ollama Service
Run the following command to start the Ollama service:

```sh
docker compose up -d Ollama
```

This command will download and run the **llama3.2:1b** model. You will need to wait for the download to complete.

#### Checking Download Progress
To check the download progress, you can either:
- Open **Docker Desktop** and check the logs for the **Ollama** container.
- Run the following command in your terminal:

```sh
docker logs ollama --follow
```

### Step 2: Start the Communicating Server
The server uses **SignalR** for real-time communication. Ensure you have the **SignalR** package installed to connect to the server.

Run the following command to start the Esrefly server:

```sh
docker compose up -d
```

The server will listen on **port 5000**, and the SignalR endpoint is:

```
/agent
```

### Step 3: Interacting with the AI Agent
- To **send messages** to the AI agent, use the `SendPrompt` method in SignalR.
- To **receive responses**, listen to the `ReceiveMessage` method in SignalR.

Now you're all set! Try out the experience yourself and run the AI model locally.
