using System;

namespace AppGM.Core
{
    /// <summary>
    /// Enum que indica la pagina actual de la aplicacion
    /// </summary>
    public enum EPagina
    {
        /// <summary>
        /// Menu principal de la aplicacion
        /// </summary>
        PaginaPrincipal = 1,

        /// <summary>
        /// Pagina del rol
        /// </summary>
        PaginaPrincipalRol = 2
    }

    /// <summary>
    /// Indica el menu actual del rol en el que esta la aplicacion
    /// </summary>
    public enum EMenuRol
    {
        /// <summary>
        /// Menu de seleccion tipo de ficha.
        /// Se trata del menu anterior a <see cref="VistaFichas"/>
        /// </summary>
	    SeleccionTipoFichas = 1,

        /// <summary>
        /// Menu de vista de fichas.
        /// Aqui se listan las fichas del tipo seleccionado en el menu <see cref="SeleccionTipoFichas"/>
        /// </summary>
        VistaFichas = 2,

        /// <summary>
        /// Vista de los mapas del rol
        /// </summary>
        Mapas = 3,

        /// <summary>
        /// Registro del rol
        /// </summary>
        Registro = 4,

        /// <summary>
        /// Menu para realizar una tirada de dados
        /// </summary>
        Tirada = 5,

        /// <summary>
        /// Menu de seleccion de combate.
        /// En este menu se listan todos los combates activos del rol.
        /// Es el paso anterior al menu <see cref="Combate"/>
        /// </summary>
        AdministrarCombates = 6,

        /// <summary>
        /// En este menu se permite controlar un combate.
        /// </summary>
        Combate = 7,

        /// <summary>
        /// No se si este valor sirva para algo pero aca esta
        /// </summary>
        NINGUNO = 0
    }

    /// <summary>
    /// Indica en que estacion del año transcurre el rol 
    /// </summary>
    public enum ETemporada
    {
        /// <summary>
        /// Temperaturas muy bajas, las posibilidades de nieve son mayores 
        /// </summary>
        Invierno = 1,

        /// <summary>
        /// Temperaturas estandares, flores y petalos por doquier 
        /// </summary>
        Primavera = 2,

        /// <summary>
        /// Temperaturas altas, naturaleza en su esplendor
        /// </summary>
        Verano = 3,

        /// <summary>
        /// Hojitas coloridas y temperaturas bajas 
        /// </summary>
        Otoño = 4,

        /// <summary>
        /// Un misterio como pudo pasar tal cosa
        /// </summary>
        NINGUNO = 0
    }

    /// <summary>
    /// Condiciones climaticas que pueden transcurrir en un dia del rol
    /// </summary>
    public enum ECondicionClimatica
    {
        /// <summary>
        /// El clima esta soleado, buena iluminacion 
        /// </summary>
        Soleado = 1,

        /// <summary>
        /// El clima esta nublado, mala iluminacion y posible humedad
        /// </summary>
        Nublado = 2,

        /// <summary>
        /// Mojado, mala visibilidad
        /// </summary>
        Lluvia = 4,

        /// <summary>
        /// Mala iluminacion, posibilidad de rayos
        /// </summary>
        TormentaElectrica = 8,

        /// <summary>
        /// Mala iluminacion, poca visibilidad y granizo :u
        /// </summary>
        Granizo = 16,

        /// <summary>
        /// Frio extremo, mala iluminacion y bajo rango de vision
        /// </summary>
        Nieve = 32,

        /// <summary>
        /// El suelo es cubierto por una ligera capa de hielo
        /// Humedad, muy bajas temperaturas
        /// </summary>
        Helada = 64,

        /// <summary>
        /// Muy humedo, la visibilidad es infima 
        /// </summary>
        Niebla = 128, 

        /// <summary>
        /// Humedo y baja visibilidad a distancia
        /// </summary>
        Neblina = 256,

        /// <summary>
        /// Airecito, puede influenciar vuelos y caidas desde sitios altos, asi como soplar objetos de poco peso
        /// </summary>
        Viento = 512,

        /// <summary>
        /// A lo mejor el mundo deja de existir, puede ser
        /// </summary>
        NINGUNO = 0
    }

    /// <summary>
    /// Posibles caracteristicas de un ambiente
    /// </summary>
    public enum ECaracteristicasAmbiente
    {
        /// <summary>
        /// Temperatura intermedia entre calurosa y fria.
        /// No ofrece ninguna ventaja o desventaja por si solo
        /// </summary>
        Templado = 1,

        /// <summary>
        /// Temperaturas sobre la media comun.
        /// Puede significar un ventaja o desventaja dependiendo de cierta habilidad.
        /// </summary>
        Caluroso = 2,

        /// <summary>
        /// Temperaturas debajo de la media comun.
        /// Puede significar un ventaja o desventaja dependiendo de cierta habilidad.
        /// </summary>
        Frio = 4,

        /// <summary>
        /// Escasa humedad en el ambiente, no tiene influencia sobre la temperatura. 
        /// </summary>
        Seco = 8,

        /// <summary>
        /// Bastante humedad en el ambiente, tiene influencia sobre la sensacion termica.
        /// </summary>
        Humedo = 16,

        /// <summary>
        /// Iliminacion completa, visibilidad completa. 
        /// </summary>
        Iluminado = 32,

        /// <summary>
        /// La visibilidad se ve empeorada por la baja iluminacion.
        /// Puede implicar desventajas en la vision o ventajas dependiendo de las habilidades del personaje.  
        /// </summary>
        Oscuro = 64,

        /// <summary>
        /// Libertad de movimiento para los personajes, casillas y pocos obstaculos. 
        /// </summary>
        Amplio = 128,

        /// <summary>
        /// El movimiento de los personajes se ve dificultado, sea por escasez en casillas o multitud de obstaculos.
        /// Desventaja en movimientos pronunciados e incapacidad de realizar algunos.
        /// </summary>
        Estrecho = 256,

        /// <summary>
        /// El personaje se siente a gusto con el lugar sea por el binestar que aporte o por la confianza en el mismo.
        /// Puede incluir ventajas y desventajas dependiendo de la habilidad.
        /// </summary>
        Comodo = 512,

        /// <summary>
        /// La inquietud respecto al ambiente es evidente para los personajes.
        /// Puede incluir ventajas y desventajas dependiendo de la habilidad.
        /// </summary>
        Ominoso = 1024,

        /// <summary>
        /// Se halla libre de cualquier objeto que dificulte el movimiento, asi como de sustancias afectando el agua y el aire.
        /// </summary>
        Limpio = 2048,

        /// <summary>
        /// La contaminacion en el aire y otros medios puede dificultar ciertas acciones.
        /// Desventajas en stats, modificadores, debuffs, incapacidad de utilizar habilidades, etc.
        /// </summary>
        Contaminado = 4096,

        /// <summary>
        /// Totalmente peculiar, solo se manifiesta en situaciones del mismo indole.
        /// Ventaja al momento de realizar una accion de ligacion sobre el mismo ambiente.
        /// </summary>
        Ligacion = 8192,

        /// <summary>
        /// Convengamos que se encuentra en el vacio, puede ser.
        /// </summary>
        NINGUNO = 0        
    }

    /// <summary>
    /// Rango de una <see cref="ModeloHabilidad"/> o <see cref="EStat"/>.
    /// Estos rangos se utilizan solo para servants o invocaciones.
    /// Los valores numericos que tienen asignados son lo que equivaldrian en stats de <see cref="ModeloMaster"/>
    /// </summary>
    public enum ERango
    {
	    /// <summary>
	    /// F, lo mas bajo de lo bajo
	    /// </summary>
	    F = 14,

	    /// <summary>
	    /// E
	    /// </summary>
	    E = 15,

	    /// <summary>
	    /// D
	    /// </summary>
	    D = 16,

	    /// <summary>
	    /// C, Agarramos mecha
	    /// </summary>
	    C = 17,

	    /// <summary>
	    /// B
	    /// </summary>
	    B = 18,

	    /// <summary>
	    /// A, Potente
	    /// </summary>
	    A = 19,

	    /// <summary>
	    /// A+, Aun mas potente
	    /// </summary>
	    AMas = 20,

	    /// <summary>
	    /// A++, Uff que polenta
	    /// </summary>
	    AMasMas = 21,

	    /// <summary>
	    /// EX, Lo mejor de lo mejor de lo mejor, y con honores
	    /// </summary>
	    Ex = 22,

	    /// <summary>
	    /// Una miseria, probablemente
	    /// </summary>
	    NINGUNO = 0
    }

    /// <summary>
    /// Sexo de un <see cref="ModeloPersonajeJugable"/>
    /// </summary>
    public enum ESexo
    {
	    /// <summary>
	    /// Mujer
	    /// </summary>
	    Femenino = 1,

	    /// <summary>
	    /// Pibe
	    /// </summary>
	    Masculino = 2,

	    /// <summary>
	    /// ???
	    /// </summary>
	    NINGUNA = 0
    }

    /// <summary>
    /// Estado de salud, higiene de un <see cref="ModeloPersonajeJugable"/>
    /// </summary>
    public enum EBienestar
    {
	    /// <summary>
	    /// Mr. Excelencia
	    /// </summary>
	    MuyBueno = 1,

	    /// <summary>
	    /// Bien
	    /// </summary>
	    Bueno = 2,

	    /// <summary>
	    /// Ni bien, ni mal
	    /// </summary>
	    Neutro = 3,

	    /// <summary>
	    /// Te vendria bien un baño
	    /// </summary>
	    Malo = 4,

	    /// <summary>
	    /// Dios mio, comete una tostada por lo menos
	    /// </summary>
	    MuyMalo = 5,

	    /// <summary>
	    /// Un asco, indistinguible de un cadaver
	    /// </summary>
	    Deplorable = 6,

	    /// <summary>
	    /// Un misterio
	    /// </summary>
	    NINGUNO = 0
    }

    /// <summary>
    /// Representa el tipo de personaje de un <see cref="ModeloPersonaje"/>
    /// </summary>
    [Flags]
    public enum ETipoPersonaje
    {
        /// <summary>
        /// Master
        /// </summary>
	    Master = 1<<0,

        /// <summary>
        /// Servant
        /// </summary>
        Servant = 1<<1,

        /// <summary>
        /// Invocacion
        /// </summary>
        Invocacion = 1<<2,

        /// <summary>
        /// NPC
        /// </summary>
        NPC = 1<<3
    }

    /// <summary>
    /// Representa la clase de un <see cref="ModeloServant"/>
    /// </summary>
    [Flags]
    public enum EClaseServant
    {
        /// <summary>
        /// Mr. Espadas
        /// </summary>
	    Saber = 1<<0, 

        /// <summary>
        /// Mr. Lanzas
        /// </summary>
        Lancer = 1<<1, 

        /// <summary>
        /// Mr. Arcos y demas armas de largo alcance
        /// </summary>
        Archer = 1<<2, 

        /// <summary>
        /// Mr. Flasheo, se puede reventar cinco cuadras de un hechizo
        /// </summary>
        Caster = 1<<3, 

        /// <summary>
        /// Mr. Puedo montar cosas
        /// </summary>
        Rider = 1<<4, 

        /// <summary>
        /// Mr. Soy un asco, te rebano la vida sin que te des cuenta
        /// </summary>
        Assassin = 1<<5, 

        /// <summary>
        /// Mr. Caliente
        /// </summary>
        Berserker = 1<<6, 

        /// <summary>
        /// Mr. Te agarro y corto el pescuezo
        /// </summary>
        Ruler = 1<<7,

        NINGUNO = 0
    }

    /// <summary>
    /// Arquetipo de la personalidad de un <see cref="ModeloPersonajeJugable"/>
    /// </summary>
    [Flags]
    public enum EArquetipo
    {
        /// <summary>
        /// Optimista y busca la felicidad. Quiere sentirse adaptado al mundo. 
        /// </summary>
	    Inocente = 1<<0,

        /// <summary>
        /// Sentido común, empatía y realismo. Todos somos iguales para él.
        /// No quiere lujos ni aspirar a algo, busca una conexión de marca empática.
        /// </summary>
        Amigo = 1<<1,

        /// <summary>
        /// Gran vitalidad y resistencia descomunal, se empeña en luchar por el poder o el honor.
        /// Detesta perder. Determinado a jamas rendirse. Podría ser demasiado ambicioso y controlador.
        /// </summary>
        Heroe = 1<<2,

        /// <summary>
        /// Se siente más fuerte que los demás y por eso prodiga una protección casi maternal sobre quienes le rodean.
        /// Desea evitar cualquier daño para quienes están bajo su égida y pretende evitar que cualquier riesgo o peligro
        /// amenace la integridad o felicidad de los demás.  Un mártir que echa en cara a los demás sus sacrificios.
        /// </summary>
        Cuidador = 1<<3,

        /// <summary>
        /// El que emprende el camino sin trazar una ruta definida, abierto siempre a la novedad y a la aventura.
        /// Tiene un afán profundo de descubrir y de descubrirse a sí mismo.
        /// En su faceta negativa es también el buscador de lo ideal que jamás está satisfecho.
        /// </summary>
        Explorador = 1<<4,

        /// <summary>
        /// Este es un transgresor, provocador y completamente independiente de la opinión de los demás.
        /// De hecho, le agrada ir en contra y piensa con cabeza propia, no por influencia ni por presión.
        /// En su faceta negativa se torna autodestructivo.
        /// </summary>
        Rebelde = 1<<5,

        /// <summary>
        /// Ama el amor y por eso disfruta prodigándolo. No solo el amor romántico, sino toda forma de amor.
        /// Su mayor dicha es sentirse amado. Disfruta de la belleza, la estética y los sentidos,
        /// de manera refinada. Hace de lo bello, en el sentido amplio, un valor superlativo.
        /// </summary>
        Amante = 1<<6,

        /// <summary>
        /// profunda ansia de libertad porque ama lo novedoso.
        /// Le encanta transformarse para hacer surgir algo totalmente nuevo, que tenga su sello.
        /// Es ocurrente, inconforme y autosuficiente. Con mucha imaginación, está lleno de genialidad.
        /// A veces es inconstante y piensa más de lo que hace.
        /// </summary>
        Creador = 1<<7,

        /// <summary>
        /// Enseña a reírse, incluso de nosotros mismos.
        /// No tiene máscaras y suele despojar de su máscara a los demás.
        /// No se toma en serio, porque lo suyo es disfrutar de la vida.
        /// En su faceta negativa puede ser libidinoso, vago y glotón.
        /// </summary>
        Bufon = 1<<8,

        /// <summary>
        /// Representa a ese librepensador que hace del intelecto y de los conocimientos su principal
        /// razón de ser y fundamento. La inteligencia y la capacidad de análisis son para él la vía regia
        /// para entenderse a sí mismo y entender al mundo.
        /// Corresponde a quien siempre tiene a mano un dato, una cita o un argumento lógico.
        /// </summary>
        Sabio = 1<<9,

        /// <summary>
        /// Regenera y renueva, no sólo para sí mismo, sino también para los demás.
        /// Él mismo está en constante proceso de transformación y crecimiento.
        /// En su faceta negativa es un enfermo que enferma a los demás.
        /// A veces llega a convertir los sucesos positivos en hechos negativos.
        /// </summary>
        Mago = 1<<10,

        /// <summary>
        /// Estable y preocupado por la excelencia, quiere que los demás hagan lo que él dice
        /// y suele tener motivos de sobra para exigir. Puede llegar a ser déspota en su afán por imponerse.
        /// </summary>
        Gobernante = 1<<11,

        //Estos no tienen una descripcion
        Sombra   = 1<<12,
        Persona  = 1<<13,
        UnoMismo = 1<<14,

        NINGUNO = 0
    }

    /// <summary>
    /// Mano dominante de un <see cref="ModeloPersonajeJugable"/>.
    /// </summary>
    [Flags]
    public enum EManoDominante
    {
        /// <summary>
        /// Mano izquierda
        /// </summary>
	    Izquierda = 1<<0, 

        /// <summary>
        /// Mano derecha
        /// </summary>
        Derecha = 1<<1, 

        /// <summary>
        /// Ambas manos
        /// </summary>
        Ambidiestro = Izquierda | Derecha,

        /// <summary>
        /// ???
        /// </summary>
        NINGUNA = 0
    }

    /// <summary>
    /// Representa una stat de un <see cref="ModeloPersonaje"/>
    /// </summary>
    [Flags]
    public enum EStat
    {
        /// <summary>
        /// Puntos de vida
        /// </summary>
	    HP = 1<<0,

        /// <summary>
        /// Fuerza fisica
        /// </summary>
        STR = 1<<1,

        /// <summary>
        /// Resistencia
        /// </summary>
        END = 1<<2,

        /// <summary>
        /// Agilidad
        /// </summary>
        AGI = 1<<3,

        /// <summary>
        /// Inteligencia
        /// </summary>
        INT = 1<<4,

        /// <summary>
        /// Suerte
        /// </summary>
        LCK = 1<<5,

        /// <summary>
        /// Personalidad
        /// </summary>
        CHR = 1<<6,

        /// <summary>
        /// Noble Phantasm
        /// </summary>
        NP = 1<<7,

        /// <summary>
        /// Nada de nada
        /// </summary>
        NINGUNA = 0
    }

    /// <summary>
    /// Indica el proposito de una habilidad
    /// </summary>
    [Flags]
    public enum EUsoDeHabilidad
    {
        /// <summary>
        /// Efecto sobre uno mismo
        /// </summary>
	    Personal = 1, 

        /// <summary>
        /// Apoyo a otro
        /// </summary>
        Soporte = 2, 

        /// <summary>
        /// Defensa
        /// </summary>
        Defensa = 4,

        /// <summary>
        /// Ataque
        /// </summary>
        Ataque = 8,

        /// <summary>
        /// Un desperdicio de espacio
        /// </summary>
        NINGUNA = 0
    }

    /// <summary>
    /// Tipo de la habilidad
    /// </summary>
    [Flags]
    public enum ETipoHabilidad
    {
        /// <summary>
        /// Habilidades de activacion automatica, son como pasivas
        /// </summary>
	    Perk = 1, 

        /// <summary>
        /// Aquellas cosas que se realizan de manera activa
        /// </summary>
        Skill = 2, 

        /// <summary>
        /// Mucha magia
        /// </summary>
        Hechizo = 4, 

        /// <summary>
        /// Son hechizos u objetos magicos de muy alto nivel
        /// </summary>
        NoblePhantasm = 8,

        /// <summary>
        /// ???
        /// </summary>
        NINGUNO = 0
    }

    /// <summary>
    /// Tipo de daño que inflige un ataque, arma, etc.
    /// </summary>
    [Flags]
    public enum ETipoDeDaño
    {
        /// <summary>
        /// <list type="bullet">
        ///     <item>Destruye o deja muy dañada defensas solidas.</item>
        ///     <item>Provoca daño colateral.</item>
        ///     <item>Depende de <see cref="EStat.STR"/> salvo excepciones concretas</item>
        /// </list>
        /// </summary>
	    Contundente = 1,

        /// <summary>
        /// <list type="bullet">
        ///     <item>Daño mas dificil de realizar</item>
        ///     <item>Lo pueden bloquear objetos bastante solidos</item>
        ///     <item>Si se inflige bien produce sangrado</item>
        /// </list>
        /// </summary>
        Cortante = 2,

        /// <summary>
        /// <list type="bullet">
        ///     <item>Perfora hasta cierto puntos defensas solidas</item>
        ///     <item>Salvo excepciones concretas depende siempre de <see cref="EStat.AGI"/></item>
        /// </list>
        /// </summary>
        Penetrante = 4,

        /// <summary>
        /// <list type="bullet">
        ///     <item>Inflige los tres tipos de daño anteriores a la vez</item>
        ///     <item>Puede llegar a tener tirada de daño fija</item>
        /// </list>
        /// </summary>
        Explosivo = 8,


        //-------------Los siguientes dos valores no estan relacionados con los anteriores-----------
        //----------------------Su unico proposito es aclarar el origen del daño---------------------


        /// <summary>
        /// Daño proveniente de un hechizo
        /// </summary>
        Magico = 16,

        /// <summary>
        /// Daño proveniente de un proyectil
        /// </summary>
        Proyectil = 32,

        /// <summary>
        /// Una miseria
        /// </summary>
        NINGUNO = 0
    }

    /// <summary>
    /// Representa una parte del cuerpo de un <see cref="ModeloPersonaje"/>
    /// </summary>
    [Flags]
    public enum EParteDelCuerpo
    {
        /// <summary>
        /// Torso
        /// </summary>
	    Torso = 1,

        /// <summary>
        /// Hombros
        /// </summary>
        Hombros = 2,

        /// <summary>
        /// Muslos
        /// </summary>
        Muslos = 4,

        /// <summary>
        /// Espalda
        /// </summary>
        Espalda = 8,

        /// <summary>
        /// Corazon
        /// </summary>
        Corazón = 16,

        /// <summary>
        /// Manos
        /// </summary>
        Manos = 32,

        /// <summary>
        /// Muñecas
        /// </summary>
        Muñecas = 64,

        /// <summary>
        /// Pies
        /// </summary>
        Pies = 128,

        /// <summary>
        /// Canillas
        /// </summary>
        Canillas = 256,

        /// <summary>
        /// Cuello
        /// </summary>
        Cuello = 512,

        /// <summary>
        /// Ingle
        /// </summary>
        Ingle = 1024,

        /// <summary>
        /// Cabeza
        /// </summary>
        Cabeza = 2048,

        /// <summary>
        /// 0, null, rei, zero, 0k, cero
        /// </summary>
        NINGUNA = 0
    }

    /// <summary>
    /// Tipo de la unidad que esta posicionada sobre el mapa, se utiliza para determinar el tipo de imagen
    /// </summary>
    [Flags]
    public enum ETipoUnidad
    {
        /// <summary>
        /// Master
        /// </summary>
	    Master = 1,

        /// <summary>
        /// Servant
        /// </summary>
	    Servant = 2,

        /// <summary>
        /// Invocacion
        /// </summary>
	    Invocacion = 4,

        /// <summary>
        /// Trampa
        /// </summary>
	    Trampa = 8,

        /// <summary>
        /// Iglesia
        /// </summary>
	    Iglesia = 16,

        /// <summary>
        /// Un fantasma con toda probabilidad
        /// </summary>
	    NINGUNO = 0
    }

    /// <summary>
    /// Representa el formato de una imagen
    /// </summary>
    public enum EFormatoImagen
    {
	    /// <summary>
	    /// PNG
	    /// </summary>
	    Png = 1,

	    /// <summary>
	    /// JPG
	    /// </summary>
	    Jpg = 2
    }
}
