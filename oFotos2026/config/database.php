<?php
// config/database.php

class Database {
    private static $conn = null;

    public static function getConnection() {
        if (self::$conn === null) {
            try {
                $tns = "(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.1.50)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)))";
                self::$conn = oci_connect('RRHH', 'RRHH', $tns, 'AL32UTF8');

                if (!self::$conn) {
                    $e = oci_error();
                    throw new Exception("Error de conexión: " . $e['message']);
                }
                
                // AQUI EJECUTAMOS EL ALTER SESSION SOLO UNA VEZ AL CREAR LA CONEXIÓN
                $stid = oci_parse(self::$conn, "ALTER SESSION SET nls_date_format = 'dd/mm/yyyy hh24:mi:ss'");
                if (!$stid) {
                    $e = oci_error(self::$conn);
                    throw new Exception("Error al preparar ALTER SESSION: " . $e['message']);
                }
                
                if (!oci_execute($stid)) {
                    $e = oci_error($stid);
                    throw new Exception("Error al ejecutar ALTER SESSION: " . $e['message']);
                }
                
                oci_free_statement($stid);
                
            } catch (Exception $e) {
                die("Error al conectar a la base de datos: " . $e->getMessage());
            }
        }
        return self::$conn;
    }

    public static function closeConnection() {
        if (self::$conn) {
            oci_close(self::$conn);
            self::$conn = null;
        }
    }
}