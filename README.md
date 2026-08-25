# ✝ God's Promises

> A multilingual digital Bible promise experience.

**God's Promises** is a web application that helps people discover Bible promises based on themes such as hope, peace, courage, strength, trust and comfort.

The application combines a simple, calm interface with a backend API that provides Bible promises in multiple languages.

---

## 🌿 About

Sometimes you simply need the right word at the right moment.

God's Promises lets users choose a theme or receive a random Bible promise.

Available themes include:

- Hope
- Peace
- Courage
- Strength
- Trust
- Comfort

The interface adapts to the selected language and retrieves the corresponding promise through the API.

---

## 🌍 Languages

The application currently supports:

- 🇩🇪 Deutsch
- 🇬🇧 English
- 🇫🇷 Français
- 🇪🇸 Español
- 🇮🇹 Italiano
- 🇵🇹 Português
- 🇷🇺 Русский
- 🇺🇦 Українська
- 🇷🇴 Română

---

## ✨ Features

- Random Bible promises
- Theme-based promises
- Multilingual interface
- Multilingual Bible content
- Language selection
- Responsive design
- Pixel-art inspired visual design
- Animated promise loading
- REST API architecture

---

## 🏗️ Architecture

The project consists of two applications:

```text
GodsPromises
│
├── GodsPromises.Web
│   └── ASP.NET Core Razor Pages
│
└── GodsPromises.API
    └── ASP.NET Core Web API

---

## 🔗 External API

God's Promises uses the open-source **Holy Bible API** as an
external source for multilingual Bible content.

The API provides access to Bible versions, books, verses,
random verses and text searches across multiple languages.

The project does not claim ownership of the external API
or its underlying Bible data.

**Holy Bible API**  
Created by Giovanni Palleschi.

Repository:  
https://github.com/gpalleschi/holybible_api

The original project is licensed under the
**GNU General Public License v3.0 (GPL-3.0)**.

Please refer to the original repository and license
for the terms applicable to the API and its contents.

---
