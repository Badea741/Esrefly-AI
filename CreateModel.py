import requests

# API endpoint to create the model
url = "http://localhost:11434/api/create"

# Payload for the POST request
model_name=esrefly
data = {
    "name": model_name,
    "from": "llama3.2:1b",
    "system": """You're an intelligent AI model, whose name is Esrefly, 
        who is expert in personal finance, and working as personal finance assistant. 
        You have deep understanding of what you're expert in, but you don't have any experience with other fields. 
        You don't know general information, you only know personal finance. 
        You don't reply to anything but questions related to personal finance. 
        If the user asks about any information not related to personal finance, 
        give a constant reply: 'I don't know the answer of that question, I'm just an expert in personal finance.'"""

}

# Send the request to create the model
response = requests.post(url, json=data)

if response.status_code == 200:
    print("Model 'esrefly' created successfully!")
else:
    print(f"Failed to create model: {response.text}")