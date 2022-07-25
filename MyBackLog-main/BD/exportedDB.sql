BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "plataforma" (
	"id_plataforma"	INTEGER NOT NULL,
	"titulo"	TEXT NOT NULL,
	"Descripcion"	TEXT,
	PRIMARY KEY("id_plataforma" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "progresion" (
	"id_progresion"	INTEGER NOT NULL,
	"nombre_estado"	TEXT NOT NULL,
	"descripcion"	TEXT,
	PRIMARY KEY("id_progresion" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "adquisicion" (
	"id_adquisicion"	INTEGER NOT NULL,
	"nombre_adquisicion"	TEXT NOT NULL,
	PRIMARY KEY("id_adquisicion" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "contenido" (
	"id_contenido"	INTEGER NOT NULL UNIQUE,
	"titulo"	TEXT NOT NULL,
	"descripcion"	TEXT,
	"calificacion"	INTEGER NOT NULL DEFAULT 0,
	"horas_inversion"	INTEGER NOT NULL DEFAULT 0,
	"id_plataforma"	INTEGER,
	"id_progresion"	INTEGER,
	"id_adquisicion"	INTEGER,
	FOREIGN KEY("id_progresion") REFERENCES "progresion"("id_progresion") ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY("id_adquisicion") REFERENCES "adquisicion"("id_adquisicion") ON DELETE SET NULL ON UPDATE CASCADE,
	FOREIGN KEY("id_plataforma") REFERENCES "plataforma"("id_plataforma") ON DELETE SET NULL ON UPDATE CASCADE,
	PRIMARY KEY("id_contenido" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "libro" (
	"id_contenido"	INTEGER NOT NULL UNIQUE,
	"cantidad_paginas"	INTEGER NOT NULL DEFAULT 0,
	"pagina"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("id_contenido") REFERENCES "contenido"("id_contenido") ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY("id_contenido")
);
CREATE TABLE IF NOT EXISTS "pelicula" (
	"id_contenido"	INTEGER NOT NULL UNIQUE,
	"duracion_minutos"	INTEGER NOT NULL DEFAULT 0,
	"minuto"	INTEGER NOT NULL,
	FOREIGN KEY("id_contenido") REFERENCES "contenido"("id_contenido") ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY("id_contenido")
);
CREATE TABLE IF NOT EXISTS "serie" (
	"id_contenido"	INTEGER NOT NULL UNIQUE,
	"tiempo_capitulo"	INTEGER NOT NULL DEFAULT 0,
	"capitulos_temporada"	INTEGER NOT NULL DEFAULT 0,
	"temporada"	INTEGER NOT NULL DEFAULT 0,
	"capitulo"	INTEGER NOT NULL DEFAULT 0,
	"minuto"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("id_contenido") REFERENCES "contenido"("id_contenido") ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY("id_contenido")
);
CREATE TABLE IF NOT EXISTS "juego" (
	"id_contenido"	INTEGER NOT NULL UNIQUE,
	FOREIGN KEY("id_contenido") REFERENCES "contenido"("id_contenido") ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY("id_contenido")
);
CREATE TABLE IF NOT EXISTS "nota" (
	"id_nota"	INTEGER NOT NULL,
	"id_juego"	INTEGER NOT NULL,
	"descripcion"	TEXT NOT NULL,
	"completado"	INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY("id_juego") REFERENCES "juego"("id_contenido") ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY("id_nota" AUTOINCREMENT)
);
INSERT INTO "progresion" ("id_progresion","nombre_estado","descripcion") VALUES (0,'No Iniciado','Aun no comienza a consumir el contenido'),
 (1,'Terminado','Se ha consumido el contenido'),
 (2,'En Progreso','Se esta consumiendo el contenido');
INSERT INTO "adquisicion" ("id_adquisicion","nombre_adquisicion") VALUES (0,'No Comprado'),
 (1,'Comprado'),
 (2,'Prestado');
COMMIT;
