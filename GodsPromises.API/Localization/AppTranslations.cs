namespace GodsPromises.Api.Localization;

public static class AppTranslations
{
    public static readonly Dictionary<string, Dictionary<string, string>> Texts = new()
    {
        ["de"] = new()
        {
            ["need_now"] = "Was brauchst du gerade?",
            ["comfort"] = "Trost",
            ["peace"] = "Frieden",
            ["hope"] = "Hoffnung",
            ["strength"] = "Stärke",
            ["trust"] = "Vertrauen",
            ["courage"] = "Mut",
            ["security"] = "Geborgenheit",
            ["random"] = "Zufällige Verheißung",
            ["another"] = "Weitere Verheißung"
        },

        ["en"] = new()
        {
            ["need_now"] = "What do you need right now?",
            ["comfort"] = "Comfort",
            ["peace"] = "Peace",
            ["hope"] = "Hope",
            ["strength"] = "Strength",
            ["trust"] = "Trust",
            ["courage"] = "Courage",
            ["security"] = "Security",
            ["random"] = "Random promise",
            ["another"] = "Another promise"
        },

        ["fr"] = new()
        {
            ["need_now"] = "De quoi as-tu besoin en ce moment ?",
            ["comfort"] = "Réconfort",
            ["peace"] = "Paix",
            ["hope"] = "Espérance",
            ["strength"] = "Force",
            ["trust"] = "Confiance",
            ["courage"] = "Courage",
            ["security"] = "Sécurité",
            ["random"] = "Promesse aléatoire",
            ["another"] = "Une autre promesse"
        },

        ["es"] = new()
        {
            ["need_now"] = "¿Qué necesitas ahora?",
            ["comfort"] = "Consuelo",
            ["peace"] = "Paz",
            ["hope"] = "Esperanza",
            ["strength"] = "Fuerza",
            ["trust"] = "Confianza",
            ["courage"] = "Valentía",
            ["security"] = "Seguridad",
            ["random"] = "Promesa aleatoria",
            ["another"] = "Otra promesa"
        },

        ["it"] = new()
        {
            ["need_now"] = "Di cosa hai bisogno in questo momento?",
            ["comfort"] = "Consolazione",
            ["peace"] = "Pace",
            ["hope"] = "Speranza",
            ["strength"] = "Forza",
            ["trust"] = "Fiducia",
            ["courage"] = "Coraggio",
            ["security"] = "Protezione",
            ["random"] = "Promessa casuale",
            ["another"] = "Un'altra promessa"
        },

        ["pt"] = new()
        {
            ["need_now"] = "Do que você precisa agora?",
            ["comfort"] = "Consolo",
            ["peace"] = "Paz",
            ["hope"] = "Esperança",
            ["strength"] = "Força",
            ["trust"] = "Confiança",
            ["courage"] = "Coragem",
            ["security"] = "Segurança",
            ["random"] = "Promessa aleatória",
            ["another"] = "Outra promessa"
        },

        ["ru"] = new()
        {
            ["need_now"] = "Что тебе сейчас нужно?",
            ["comfort"] = "Утешение",
            ["peace"] = "Мир",
            ["hope"] = "Надежда",
            ["strength"] = "Сила",
            ["trust"] = "Доверие",
            ["courage"] = "Мужество",
            ["security"] = "Защита",
            ["random"] = "Случайная обетование",
            ["another"] = "Другое обетование"
        },

        ["uk"] = new()
        {
            ["need_now"] = "Що тобі зараз потрібно?",
            ["comfort"] = "Розрада",
            ["peace"] = "Мир",
            ["hope"] = "Надія",
            ["strength"] = "Сила",
            ["trust"] = "Довіра",
            ["courage"] = "Мужність",
            ["security"] = "Захист",
            ["random"] = "Випадкова обітниця",
            ["another"] = "Інша обітниця"
        },

        ["ro"] = new()
        {
            ["need_now"] = "De ce ai nevoie acum?",
            ["comfort"] = "Mângâiere",
            ["peace"] = "Pace",
            ["hope"] = "Speranță",
            ["strength"] = "Putere",
            ["trust"] = "Încredere",
            ["courage"] = "Curaj",
            ["security"] = "Siguranță",
            ["random"] = "Promisiune aleatorie",
            ["another"] = "O altă promisiune"
        }
    };

    public static string Get(string language, string key)
    {
        language = language.Trim().ToLowerInvariant();

        if (Texts.TryGetValue(language, out var languageTexts) &&
            languageTexts.TryGetValue(key, out var value))
        {
            return value;
        }

        // Fallback auf Deutsch
        return Texts["de"].TryGetValue(key, out var fallback)
            ? fallback
            : key;
    }
}