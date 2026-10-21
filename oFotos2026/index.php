<?php
// index.php

// Configuración inicial
session_start();
error_reporting(E_ALL);
ini_set('display_errors', 1);

// Configuración de la base de datos
require_once __DIR__ . '/config/database.php';

// Cargar controlador principal
require_once __DIR__ . '/app/controllers/FotoController.php';

// Crear conexión a la base de datos
$database = new Database();
$db = $database->getConnection();

// Crear instancia del controlador
$controller = new FotoController($db);

date_default_timezone_set('America/Guatemala');

// Manejar la acción solicitada
$action = $_GET['action'] ?? 'showMain';

// Router simple
switch ($action) {
    case 'showMain':
        $controller->mostrarCaptura();
        break;

    case 'capturaFoto':
        $controller->mostrarCaptura();
        break;

    case 'guardarFoto':
        $controller->guardarFoto();
        break;

    default:
        $controller->mostrarCaptura();
        break;
}