use BD_Project;

-- bit = positivo/negativo
-- d = dropdown
go
create schema ProjetoConsultorio;
go
-- Paciente
create table ProjetoConsultorio.Paciente(
	PacNIF			int				not null,
	Nome			varchar(50)		not null,
	DataNascimento	date			not null,
	Residencia		varchar(50),
	CodigoPostal	varchar(20),
	Contactos		varchar(50)		not null,
	Mail			varchar(50)		not null	unique,
	Profissao		varchar(50),
	EstadoCivil		varchar(20), -- d(casada/solteira/uniao de facto/divorciada/viuva)
	
	SS				int				not null	unique,
	SNS				int				not null	unique,
	Subsistema		varchar(100),
	
	constraint PPK primary key(PacNIF),
	constraint PTPK check(len(PacNIF)=8),
	constraint PN check(len(Nome)>3),
	constraint PM check(len(Mail)>6),
);

-- Historial Clinico
create table ProjetoConsultorio.HistorialClinico (
	PacNIF				int				not null,
	GrupoSanguineo		varchar(10)		not null,		-- d (A/B/AB/0 Rh +/-)
	Altura				real			not null,
	Peso				real			not null,		-- ver formula para calcular IMC
	AntFamiliares		varchar(500),
	AntPessoais			varchar(500),
	Cirurgias			varchar(500),
	Alergias			varchar(500),
	TransfSanguineas	bit				not null,
	Tabaco				varchar(500),
	Alcool				varchar(500),
	Medicamentos		varchar(500),
	Menarca				int,
	Coitarca			int,
	Ciclos				varchar(500),
	Contracecao			varchar(500),	
	Gravidezes			int				default 0,
	Partos				int				default 0,
	HistoriaObstetrica	varchar(1000),
	DUM					date,
	DPP					date,
	Menopausa			int,
	
	constraint HCPK primary key(PacNIF, GrupoSanguineo),
	constraint HCPFK foreign key(PacNIF) references ProjetoConsultorio.Paciente(PacNIF) ON UPDATE CASCADE,
	constraint HCA check(Altura>0.54),
	constraint HCP check(Peso>0),
);

-- Gravidez
create table ProjetoConsultorio.Gravidez(
	NIFPac				int		not null,
	NumeroGravidez		int		not null,
	NumeroConsultas		int		not null,

	constraint GGN check(NumeroGravidez>0),
	constraint GPK primary key(NIFPac, NumeroGravidez),
	constraint GPFK foreign key(NIFPac) references ProjetoConsultorio.Paciente(PacNIF) ON UPDATE CASCADE,
);

-- Medico
create table ProjetoConsultorio.Medico (
	MedNIF				int				not null,
	Nome				varchar(50)		not null,
	Mail				varchar(50)		not null	unique,
	Especialidade		varchar(50)		not null,
	HorasSemanais		int				not null,
	DataInicio			date			not null,
	Salario				real			not null,

	constraint MPK	primary key(MedNIF),
	constraint MN	check(len(Nome)>3),
	constraint MM	check(len(Mail)>6),
	constraint MHS	check(HorasSemanais>0),
	constraint MS	check(Salario>0),
);

-- Consulta
create table ProjetoConsultorio.Consulta(
	ID				int		not null,
	DataC			date 	not null,
	Hora			real	not null,
	NIFPaciente		int		not null,

	constraint CPK primary key(ID),
	constraint CPFK foreign key(NIFPaciente) references ProjetoConsultorio.Paciente(PacNIF) ON UPDATE CASCADE,
);

-- Consulta Ginecologia
create table ProjetoConsultorio.ConsultaGinecologia(
	ID				int				not null,
	Descricao		varchar(1000),

	constraint CGPK primary key(ID),
	constraint CGCFK foreign key(ID) references ProjetoConsultorio.Consulta(ID) ON UPDATE CASCADE,
);

-- Dirige
create table ProjetoConsultorio.Dirige(
	Medico			int		not null,
	Consulta		int		not null,

	constraint DPK primary key(Medico, Consulta),
	constraint DMFK foreign key(Medico) references ProjetoConsultorio.Medico(MedNIF) ON UPDATE CASCADE,
	constraint DCFK foreign key(Consulta) references ProjetoConsultorio.Consulta(ID) ON UPDATE CASCADE,
);

-- Consulta Obstetricia
create table ProjetoConsultorio.ConsultaObstetricia (
	ID				int				not null,
	GravidezPac		int				not null,
	GravidezNum		int				not null,
	Tipo			varchar(20)		not null,

	-- IG				, -- semanas e dias entre data da ultima menstruacao e data atual
	-- IGEco			, -- semanas e dias entre data de uma ecografia e data atual (dropdown com ecografias ja feitas para escolher a que interessa)
	AFU				int,
	MAF				bit,
	Foco			bit,
	Peso			real			not null,
	TA				varchar(20),  -- int / int 
	ExGin			varchar(500),
	Queixas			varchar(500),
	Medicacao		varchar(500),
	Obs				varchar(1000),

	--consultaPuerperio
	PuerAmamentacao varchar(100),
	PuerMenstruacao varchar(100),
	PuerObservacoes varchar(500),

	constraint COPK primary key(ID),
	constraint COCFK foreign key(ID) references ProjetoConsultorio.Consulta(ID) ON UPDATE CASCADE,
	constraint COGFK foreign key(GravidezPac, GravidezNum) references ProjetoConsultorio.Gravidez(NIFPac, NumeroGravidez) ON UPDATE no action,
);

-- Recém-nascido
create table ProjetoConsultorio.Bebe(
	NIFPac			int				not null,
	NumGravidez		int				not null,
	Bebe			int				not null		default 1,
	Parto			varchar(20),  -- d (eutocico/ventosa/forceps/cesariana/pelvico)
	Indicacao 		varchar(500),
	SexoBebe 		varchar(15),  -- d (Masculino/Feminino/Indeterminado)
	PesoBebe 		real,
	APGAR 			varchar(20),

	constraint BPK primary key(NIFPac, NumGravidez),
	constraint BFK foreign key(NIFPac, NumGravidez) references ProjetoConsultorio.Gravidez(NIFPac, NumeroGravidez) ON UPDATE CASCADE,
	constraint BP check(PesoBebe>0),
);

-- Requisicao Analise
create table ProjetoConsultorio.RequisicaoAnalise(
	IDRA			int			not null,
	DataR			date		not null,
	NIFPaciente		int			not null,
	NIFMedico		int,
	Tipo			varchar(30)	not null,

	constraint RAPK primary key(IDRA),
	constraint RAMFK foreign key(NIFMedico) references ProjetoConsultorio.Medico(MedNIF) ON UPDATE CASCADE,
	constraint RAPFK foreign key(NIFPaciente) references ProjetoConsultorio.Paciente(PacNIF) ON UPDATE CASCADE,
);

-- Descricao Analise
create table ProjetoConsultorio.DescricaoAnalise(
	IDDA			int				not null,
	Consulta		int				not null,
	Requisicao		int,
	DataDA			date			not null,
	Descricao		varchar(500),

	constraint DAPK primary key(IDDA),
	constraint DACFK foreign key(Consulta) references ProjetoConsultorio.Consulta(ID) ON UPDATE CASCADE,
	constraint DARAFK foreign key(Requisicao) references ProjetoConsultorio.RequisicaoAnalise(IDRA) ON UPDATE no action,
);

-- EcografiaGinecologica
create table ProjetoConsultorio.EcografiaGinecologica(
	IDDA		int				not null,

	constraint EGDAPK primary key(IDDA),
	constraint EGDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- EcografiaObstetrica
create table ProjetoConsultorio.EcografiaObstetrica(
	IDDA		int				not null,
	Semana		int,
	Dias		int,

	constraint EODAPK primary key(IDDA),
	constraint EODAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- ECG
create table ProjetoConsultorio.ECG(
	IDDA		int			not null,

	constraint ECGDAPK primary key(IDDA),
	constraint ECGDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- HSG
create table ProjetoConsultorio.HSG(
	IDDA		int			not null,

	constraint HDAPK primary key(IDDA),
	constraint HDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- Espermograma
create table ProjetoConsultorio.Espermograma(
	IDDA		int			not null,

	constraint EDAPK primary key(IDDA),
	constraint EDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- CMColo
create table ProjetoConsultorio.CMColo(
	IDDA			int			not null,
	Convencional	varchar(100),
	MeioLiquido		varchar(100),

	constraint CCDAPK primary key(IDDA),
	constraint CMDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- RX
create table ProjetoConsultorio.RX(	
	IDDA		int			not null,
	Tipo		varchar(100),

	constraint RXDAPK primary key(IDDA),
	constraint RXDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- Mama
create table ProjetoConsultorio.Mama(
	IDDA				int			not null,
	Mamografia			varchar(100),
	EcografiaMamaria	varchar(100),

	constraint MDAPK primary key(IDDA),
	constraint MDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- Exsudado
create table ProjetoConsultorio.Exsudado(
	IDDA					int			not null,
	VaginalBacteriologico	varchar(50),
	VaginalMicologico		varchar(50),
	VaginalParasitologico	varchar(50),
	VaginoRetalSGB			bit,

	constraint EXDAPK primary key(IDDA),
	constraint EXDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- Urina
create table ProjetoConsultorio.Urina(
	IDDA			int 			not null,
	SumariaUrinas	varchar(20),
	Urocultura		bit,
	ValorUroc		varchar(20)     default null, --show if urocultura=true
	TIG				bit,

	constraint UDAPK primary key(IDDA),
	constraint UDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);

-- Sangue
create table ProjetoConsultorio.Sangue(
	IDDA						int 			not null,

	Rastreio1Trim				varchar(50), 
	Rastreio2Trim				varchar(50),

	GrupoSanguineo				varchar(10),		-- d (A/B/AB/0 Rh +/-)
	CoombsIndireto				bit,
	HemogramaHemoglobina		real,
	Plaquetas					int,
	TP							varchar(20),
	TTP							varchar(20),
	VS							real,

	Glicose						real,
	PTGO						varchar(20),
	Ureia						real,
	Creatinina					real,
	AcidoUrico					real,
	FosfataseAlcalina			real,
	BilirubinasDireta			real,
	BilirubinasIndireta			real,
	TGO							real,
	TGP							real,
	
	ColestTotal					real,
	Triglicerideos				real,				--corrigir DER
	HDL							real,
	LDL							real,

	VDRL						bit,
	SerologiaCMV_IgG			varchar(20),		-- d (imune/nao-imune/indeterminado)
	SerologiaCMV_IgM			varchar(20),		-- d (sem/com infeccao recente)
	SerologiaRubeola_IgG		varchar(20),		-- d (imune/nao-imune/indeterminado)
	SerologiaRubeola_IgM		varchar(20),		-- d (sem/com infeccao recente)
	SerologiaToxoplasmose_IgG	varchar(20),		-- d (imune/nao-imune/indeterminado)
	SerologiaToxoplasmose_IgM	varchar(20),		-- d (sem/com infeccao recente)
	AgHBs						bit,
	HCV							bit,
	HIV							bit,
	
	TSH							real,
	T3							real,
	T4							real,
	FSH							real,
	LH							real,
	Estradiol					real,
	Progesterona				real,
	Prolactina					real,
	DHEA						real,
	Testosterona				real,
	
	CA125						real,
	CA15_3						real,
	CA19_9						real,
	CEA							real,

	AntiCoagulanteLupico		bit,
	Antifosfolipidico			bit,
	Antitrombina				bit,
	AntiDNA						bit,
	AntiCardiolipina			bit,
	AntiGliadina				bit,
	AntiNucleares				bit,
	AntiEndomisio				bit,
	AntiReticulina				bit,
	AntiTransglutaminase		bit,
	ProteinaC					bit,
	ProteinaS					bit,
	Imunoglobulinas				bit,
	Glicoproteina				bit,

	Outros						varchar(200),

	constraint SDAPK primary key(IDDA),
	constraint SDAFK foreign key(IDDA) references ProjetoConsultorio.DescricaoAnalise(IDDA) ON UPDATE CASCADE,
);