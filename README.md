# Oscar Miranda- Filtro Ropa

  Para las consultas se realizaron 6 metodos pero solo 5 estan implementados en los controladores
  * Listar los insumos que pertenecen a una prenda especifica. El usuario debe ingresar el código de la prenda.
    - Ruta: http://localhost:5213/api/Insumo/Xprenda/223
      * Respuesta
      ![image](https://github.com/OSCARJMG23/OscarMiranda-Ropa/assets/133609079/05c5e85b-4b19-4525-a3bf-b8cf9eed0109)

   
  * Listar los Insumos que son vendidos por un determinado proveedor cuyo tipo de persona sea Persona Jurídica. El usuario debe ingresar el Nit de proveedor.
    - Ruta: http://localhost:5213/api/Insumo/Xproveedor/1234
      * Respuesta
      ![image](https://github.com/OSCARJMG23/OscarMiranda-Ropa/assets/133609079/69b6f630-e4a9-4d9b-b480-2ea3820e2e0d)

      
  * Listar todas las ordenes de producción cuyo estado se en proceso.
    - Ruta: http://localhost:5213/api/Orden/proceso
      * Respuesta
      
      ![image](https://github.com/OSCARJMG23/OscarMiranda-Ropa/assets/133609079/1360643e-216c-40ea-b1a0-b02fd735ac9c)
      Nota: Se cumple la consulta con las ordenes que su estado esta en proceso, pero el mapeado no da la informacion completa.
      
   
  * Listar los empleados por un cargo especifico. Los cargos que se encuentran en la empresa son: Auxiliar de Bodega, Jefe de Producción, Corte, Jefe de bodega, Secretaria, Jefe de IT.
    - Ruta: http://localhost:5213/api/Empleado/cargo/Auxiliar de Bodega
      * Respuesta
      
      ![image](https://github.com/OSCARJMG23/OscarMiranda-Ropa/assets/133609079/329b29bf-9a4a-4412-8a19-c2bac670cd20)
  
  * Listar las ordenes de producción que pertenecen a un cliente especifico. El usuario debe ingresar el IdCliente y debe obtener la siguiente información:
    1. IdCliente, Nombre, Municipio donde se encuentra ubicado.
    2. Nro de orden de producción, fecha y el estado de la orden de producción (Se debe mostrar la descripción del estado, código del estado, valor total de la orden de producción.
    3. Detalle de orden: Nombre de la prenda, Código de la prenda, Cantidad, Valor total en pesos y en dólares.
    - Ruta: http://localhost:5213/api/Orden/cliente/12345
      * Respuesta
      
      ![image](https://github.com/OSCARJMG23/OscarMiranda-Ropa/assets/133609079/3b8db4cc-e27a-4805-9e86-53073626a11d)
    Nota: Se cumple con la consulta, pero no el mapeado requerido

  * Rutas para probar el funcionamiento de jwt
    - Register: http://localhost:5213/api/User/register
    - AddRol: http://localhost:5213/api/User/addrole
    - Create Token: http://localhost:5213/api/User/token
    - Refresh Token: http://localhost:5213/api/User/refresh-token

## Requerimientos que se cumplieron
  * Todos los Crud
  * Paginacion para todos los metodos get de cada controlador.(para hacer la consulta por query, en algunos es por id, descripcion, nombre y nit).
  * Rate limit
  * Authorizacion jwt y su refresh token
  * 5 consultas. (contando las 2 que no cumplen con el mapeo requerido).
    
## Requerimientos que no se cumplieron
  * Versionado a todos lo controladores. (se hizo la debida configuracion del versionado, pero no se implementó a ningun controlador).
  * 2 Consultas

  Nota: El proyecto contiene la migracion hecha, donde se llena la base de datos para probar las consultas realizadas

  
 
