using UnityEngine;

public static class WordDatabase
{
    private static readonly string[] Words =
    {
        "chat", "chien", "maison", "arbre", "soleil", "lune", "route", "ville", "porte", "table",
        "chaise", "livre", "papier", "crayon", "verre", "pierre", "fleur", "plage", "neige", "vent",
        "feu", "eau", "terre", "air", "montagne", "foret", "riviere", "nuage", "orage", "etoile",
        "matin", "soir", "jour", "nuit", "heure", "minute", "seconde", "temps", "monde", "pays",
        "fruit", "pomme", "poire", "banane", "orange", "citron", "raisin", "tomate", "carotte", "salade",
        "pain", "fromage", "sucre", "sel", "poivre", "viande", "poisson", "poulet", "riz", "pate",
        "ecole", "classe", "cours", "prof", "eleve", "cahier", "stylo", "sac", "bureau", "fenetre",
        "travail", "metier", "argent", "prix", "vente", "achat", "client", "projet", "equipe", "reunion",
        "ami", "famille", "pere", "mere", "frere", "soeur", "enfant", "homme", "femme", "voisin",
        "corps", "main", "pied", "tete", "bras", "jambe", "doigt", "oeil", "bouche", "nez",
        "rouge", "bleu", "vert", "jaune", "noir", "blanc", "gris", "rose", "marron", "violet",
        "grand", "petit", "large", "court", "long", "rapide", "lent", "fort", "faible", "calme",
        "chaud", "froid", "doux", "dur", "clair", "sombre", "propre", "sale", "jeune", "vieux",
        "marcher", "courir", "sauter", "tomber", "monter", "descendre", "ouvrir", "fermer", "prendre", "donner",
        "voir", "dire", "faire", "aller", "venir", "partir", "rester", "penser", "croire", "aimer",
        "parler", "ecouter", "lire", "ecrire", "jouer", "gagner", "perdre", "chercher", "trouver", "attendre",
        "image", "photo", "video", "musique", "chanson", "film", "scene", "radio", "bruit", "son",
        "animal", "cheval", "vache", "mouton", "cochon", "lapin", "oiseau", "canard", "poule", "chaton",
        "voiture", "camion", "train", "avion", "bateau", "velo", "moto", "bus", "taxi", "garage",
        "jardin", "champ", "ferme", "lac", "mer", "ocean", "ile", "pont", "mur", "toit",
        "outil", "marteau", "clou", "vis", "corde", "cle", "boite", "lampe", "prise", "cable",
        "jeu", "score", "niveau", "bonus", "danger", "mission", "combat", "attaque", "defense", "victoire",
        "hasard", "chance", "risque", "choix", "idee", "plan", "ordre", "liste", "ligne", "forme",
        "centre", "cote", "bord", "coin", "fond", "haut", "bas", "milieu", "avant", "arriere",
        "simple", "double", "triple", "entier", "moitie", "nombre", "chiffre", "calcul", "point", "signe",
        "memoire", "esprit", "raison", "envie", "peur", "joie", "rire", "colere", "doute", "secret",
        "voyage", "valise", "hotel", "carte", "billet", "depart", "arrivee", "chemin", "visite", "retour",
        "repas", "dessert", "soupe", "boisson", "cafe", "the", "lait", "beurre", "oeuf", "farine",
        "chemise", "pantalon", "veste", "robe", "jupe", "chaussure", "chapeau", "gant", "poche", "ceinture",
        "telephone", "clavier", "ecran", "souris", "fichier", "dossier", "reseau", "serveur", "logiciel", "machine",
        "minute", "systeme", "moteur", "energie", "lumiere", "ombre", "vitesse", "force", "poids", "volume",
        "nature", "plante", "branche", "racine", "feuille", "graine", "herbe", "mousse", "boue", "sable",
        "hiver", "printemps", "ete", "automne", "saison", "pluie", "gel", "chaleur", "fraicheur", "brouillard",
        "justice", "regle", "loi", "droit", "devoir", "preuve", "accord", "contrat", "avis", "decision",
        "sante", "repos", "sport", "course", "nage", "marche", "danse", "effort", "fatigue", "sommeil",
        "atelier", "usine", "magasin", "marche", "banque", "poste", "mairie", "hopital", "gare", "parking",
        "histoire", "phrase", "texte", "mot", "lettre", "langue", "voix", "accent", "ton", "message",
        "royaume", "chateau", "village", "place", "tour", "statue", "fontaine", "eglise", "palais", "ruine",
        "bijou", "or", "argent", "cuivre", "metal", "bois", "verre", "tissu", "cuir", "plastique",
        "sourire", "regard", "visage", "coeur", "ame", "reve", "souvenir", "memoire", "avenir", "passe",
        "adresse", "numero", "code", "motif", "objet", "piece", "salle", "couloir", "etage", "cave",
        "grenier", "balcon", "salon", "cuisine", "chambre", "douche", "bain", "miroir", "serviette", "savon",
        "coffre", "tiroir", "placard", "armoire", "canape", "tapis", "rideau", "coussin", "matelas", "oreiller",
        "miel", "confiture", "cereale", "legume", "haricot", "pois", "oignon", "ail", "basilic", "persil",
        "cerise", "fraise", "melon", "peche", "abricot", "prune", "noix", "amande", "datte", "figue",
        "renard", "loup", "ours", "singe", "tigre", "lion", "zebre", "girafe", "elephant", "serpent",
        "abeille", "fourmi", "mouche", "papillon", "araignee", "escargot", "grenouille", "lezard", "requin", "baleine",
        "foudre", "tonnerre", "tempete", "vague", "maree", "courant", "falaise", "colline", "vallee", "grotte",
        "patience", "courage", "talent", "humour", "respect", "calme", "chance", "limite", "erreur", "succes",
        "debut", "fin", "suite", "pause", "action", "reaction", "signal", "alerte", "niveau", "record",
        "facile", "normal", "difficile", "extreme", "rapide", "precis", "mobile", "visible", "stable", "solide"
    };

    public static string GetRandomWord()
    {
        return Words[Random.Range(0, Words.Length)];
    }
}