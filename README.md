# EyeOfJanthir

A .NET 10 monitoring app that polls Portainer API to track environments and containers, sending Discord notifications on state changes.

## Table of Contents

- [Features](#features)
- [Notification Rules](#notification-rules)
- [Configuration](#configuration)
- [Getting Started](#getting-started)

## Features

- Periodic Portainer API polling at configurable intervals
- Environment and container health monitoring
- Real-time Discord webhook notifications

## Notification Rules

| Rule | Trigger |
|------|---------|
| `NotifyEnvironmentIsDown` | Environment goes down or detected down on first check |
| `NotifyEnvironmentChangedUp` | Environment recovers from down state |
| `NotifyContainerIsDown` | Container stops or detected stopped on first check |
| `NotifyContainerChangedUp` | Container starts after being stopped |

## Configuration

Create a `.env` file in the root directory:

```env
Portainer__Url=https://portainer.example.com
Portainer__ApiKey=your-api-key
Portainer__Frequency=00:01:00
Discord__WebhookUrl=https://discord.com/api/webhooks/your-webhook
```

| Variable | Required | Description |
|----------|----------|-------------|
| `Portainer__Url` | Yes | Portainer instance URL |
| `Portainer__ApiKey` | Yes | Portainer API key |
| `Portainer__Frequency` | Yes | Polling interval (e.g., `00:01:00`) |
| `Discord__WebhookUrl` | Yes | Discord webhook URL |

## Getting Started

1. Clone the repository
2. Create a `.env` file with your configuration (based on .env.example)
3. Run: `docker compose up -d`