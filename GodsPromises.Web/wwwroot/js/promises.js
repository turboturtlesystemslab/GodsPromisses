"use strict";

/* ============================================================
   GOD'S PROMISES
   Frontend Logic
   ============================================================ */

const API_BASE = "https://localhost:7101/api";


/* ============================================================
   THEMES
   ============================================================ */

const themes = {

    hope: {
        de: "Hoffnung",
        en: "Hope",
        fr: "Espoir",
        es: "Esperanza",
        it: "Speranza",
        pt: "Esperança",
        ru: "Надежда",
        uk: "Надія",
        ro: "Speranță"
    },

    peace: {
        de: "Frieden",
        en: "Peace",
        fr: "Paix",
        es: "Paz",
        it: "Pace",
        pt: "Paz",
        ru: "Мир",
        uk: "Мир",
        ro: "Pace"
    },

    courage: {
        de: "Mut",
        en: "Courage",
        fr: "Courage",
        es: "Valor",
        it: "Coraggio",
        pt: "Coragem",
        ru: "Мужество",
        uk: "Мужність",
        ro: "Curaj"
    },

    strength: {
        de: "Stärke",
        en: "Strength",
        fr: "Force",
        es: "Fuerza",
        it: "Forza",
        pt: "Força",
        ru: "Сила",
        uk: "Сила",
        ro: "Putere"
    },

    trust: {
        de: "Vertrauen",
        en: "Trust",
        fr: "Confiance",
        es: "Confianza",
        it: "Fiducia",
        pt: "Confiança",
        ru: "Доверие",
        uk: "Довіра",
        ro: "Încredere"
    },

    comfort: {
        de: "Trost",
        en: "Comfort",
        fr: "Réconfort",
        es: "Consuelo",
        it: "Consolazione",
        pt: "Consolo",
        ru: "Утешение",
        uk: "Потіха",
        ro: "Mângâiere"
    }
};


/* ============================================================
   UI TRANSLATIONS
   ============================================================ */

const translations = {

    de: {
        question: "Was brauchst du gerade?",
        random: "Zufällige Verheißung",
        loading: "Eine Verheißung wird geladen …",
        waiting: "Eine Verheißung wartet auf dich.",
        word: "Gottes Wort",
        error: "Die Verheißung konnte momentan nicht geladen werden."
    },

    en: {
        question: "What do you need right now?",
        random: "Random promise",
        loading: "Loading a promise …",
        waiting: "A promise is waiting for you.",
        word: "God's Word",
        error: "The promise could not be loaded."
    },

    fr: {
        question: "De quoi as-tu besoin en ce moment ?",
        random: "Promesse aléatoire",
        loading: "Chargement d'une promesse …",
        waiting: "Une promesse t'attend.",
        word: "La Parole de Dieu",
        error: "La promesse n'a pas pu être chargée."
    },

    es: {
        question: "¿Qué necesitas en este momento?",
        random: "Promesa aleatoria",
        loading: "Cargando una promesa …",
        waiting: "Una promesa te espera.",
        word: "La Palabra de Dios",
        error: "No se pudo cargar la promesa."
    },

    it: {
        question: "Di cosa hai bisogno in questo momento?",
        random: "Promessa casuale",
        loading: "Caricamento di una promessa …",
        waiting: "Una promessa ti aspetta.",
        word: "La Parola di Dio",
        error: "Non è stato possibile caricare la promessa."
    },

    pt: {
        question: "Do que você precisa neste momento?",
        random: "Promessa aleatória",
        loading: "Carregando uma promessa …",
        waiting: "Uma promessa espera por você.",
        word: "A Palavra de Deus",
        error: "Não foi possível carregar a promessa."
    },

    ru: {
        question: "Что тебе сейчас нужно?",
        random: "Случайная обетование",
        loading: "Загружается обетование …",
        waiting: "Тебя ждёт обетование.",
        word: "Слово Божье",
        error: "Не удалось загрузить обетование."
    },

    uk: {
        question: "Що тобі зараз потрібно?",
        random: "Випадкова обітниця",
        loading: "Завантаження обітниці …",
        waiting: "На тебе чекає обітниця.",
        word: "Слово Боже",
        error: "Не вдалося завантажити обітницю."
    },

    ro: {
        question: "De ce ai nevoie acum?",
        random: "Promisiune aleatorie",
        loading: "Se încarcă o promisiune …",
        waiting: "O promisiune te așteaptă.",
        word: "Cuvântul lui Dumnezeu",
        error: "Promisiunea nu a putut fi încărcată."
    }
};


/* ============================================================
   ELEMENTS
   ============================================================ */

const languageSelect =
    document.getElementById("languageSelect");

const themeButtons =
    document.getElementById("themeButtons");

const questionText =
    document.getElementById("questionText");

const promiseText =
    document.getElementById("promiseText");

const promiseReference =
    document.getElementById("promiseReference");

const promiseTheme =
    document.getElementById("promiseTheme");

const promiseButton =
    document.getElementById("promiseButton");

const buttonText =
    document.getElementById("buttonText");

const errorMessage =
    document.getElementById("errorMessage");


/* ============================================================
   CURRENT LANGUAGE
   ============================================================ */

let currentLanguage =
    localStorage.getItem("godsPromisesLanguage") || "de";

if (!translations[currentLanguage]) {
    currentLanguage = "de";
}

languageSelect.value = currentLanguage;


/* ============================================================
   RENDER THEMES
   ============================================================ */

function renderThemes() {

    themeButtons.innerHTML = "";

    Object.keys(themes).forEach(themeKey => {

        const button =
            document.createElement("button");

        button.type = "button";

        button.className =
            "gp-theme-button";

        button.dataset.theme =
            themeKey;

        button.textContent =
            themes[themeKey][currentLanguage];

        button.addEventListener(
            "click",
            () => loadTheme(themeKey)
        );

        themeButtons.appendChild(button);
    });
}


/* ============================================================
   UPDATE INTERFACE
   ============================================================ */

function updateInterface() {

    const language =
        translations[currentLanguage];

    questionText.textContent =
        language.question;

    buttonText.textContent =
        language.random;

    renderThemes();
}


/* ============================================================
   SHOW PROMISE
   ============================================================ */

function showPromise(data) {

    promiseText.classList.remove("promise-change");

    void promiseText.offsetWidth;

    promiseText.textContent =
        `„${data.text}“`;

    promiseReference.textContent =
        data.reference ||
        translations[currentLanguage].word;

    if (data.theme) {

        const themeKey =
            data.theme.toLowerCase();

        if (themes[themeKey]) {

            promiseTheme.textContent =
                themes[themeKey][currentLanguage];

        } else {

            promiseTheme.textContent =
                data.theme;
        }

    } else {

        promiseTheme.textContent = "";
    }

    errorMessage.hidden = true;

    promiseText.classList.add("promise-change");
}


/* ============================================================
   LOADING
   ============================================================ */

function showLoading() {

    const language =
        translations[currentLanguage];

    promiseText.textContent =
        `„${language.loading}“`;

    promiseReference.textContent =
        "…";

    promiseTheme.textContent =
        "";

    errorMessage.hidden = true;
}


/* ============================================================
   ERROR
   ============================================================ */

function showError() {

    const language =
        translations[currentLanguage];

    promiseText.textContent =
        `„${language.error}“`;

    promiseReference.textContent =
        language.word;

    promiseTheme.textContent =
        "";

    errorMessage.textContent =
        language.error;

    errorMessage.hidden = false;
}


/* ============================================================
   BUTTON LOADING STATE
   ============================================================ */

function setLoadingState(isLoading) {

    promiseButton.disabled =
        isLoading;

    promiseButton.classList.toggle(
        "loading",
        isLoading
    );
}


/* ============================================================
   RANDOM PROMISE
   ============================================================ */

async function loadRandomPromise() {

    showLoading();

    setLoadingState(true);

    try {

        const url =
            `${API_BASE}/Promises/random?language=${encodeURIComponent(currentLanguage)}`;

        const response =
            await fetch(url);

        if (!response.ok) {

            throw new Error(
                `HTTP ${response.status}`
            );
        }

        const data =
            await response.json();

        showPromise(data);

    } catch (error) {

        console.error(
            "Random promise error:",
            error
        );

        showError();

    } finally {

        setLoadingState(false);
    }
}


/* ============================================================
   THEME PROMISE
   ============================================================ */

async function loadTheme(themeKey) {

    showLoading();

    setLoadingState(true);

    try {

        const url =
            `${API_BASE}/Promises/theme/${encodeURIComponent(themeKey)}?language=${encodeURIComponent(currentLanguage)}`;

        const response =
            await fetch(url);

        if (!response.ok) {

            throw new Error(
                `HTTP ${response.status}`
            );
        }

        const data =
            await response.json();

        showPromise(data);

    } catch (error) {

        console.error(
            "Theme promise error:",
            error
        );

        showError();

    } finally {

        setLoadingState(false);
    }
}


/* ============================================================
   LANGUAGE CHANGE
   ============================================================ */

languageSelect.addEventListener(
    "change",
    async event => {

        currentLanguage =
            event.target.value;

        localStorage.setItem(
            "godsPromisesLanguage",
            currentLanguage
        );

        updateInterface();

        await loadRandomPromise();
    }
);


/* ============================================================
   RANDOM BUTTON
   ============================================================ */

promiseButton.addEventListener(
    "click",
    loadRandomPromise
);


/* ============================================================
   INITIALIZE
   ============================================================ */

updateInterface();

loadRandomPromise();