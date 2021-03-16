import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.*;
import java.util.LinkedHashMap;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.concurrent.ThreadLocalRandom;

public class Insertions {
    
    private final static int[] iterNum = {800,1000,500};
    private final static char[] alfabeto = {'a','b','c','d','e','f','g','h','i','j','l','m','n','o','p','q','r','s','t','u','v','x','z'};
    private final static String[] cidades = {"Aveiro","Porto","Coimbra","Lisboa","Viseu","Guimaraes"};
    private final static String[] profissoes = {"Medica","Auxiliar de Escritorio","Assistente Administrativa", "Comerciante", "Operadora de Caixa", "Professora", "Enfermeira", "Recepcionista", "Cozinheira"};
    private final static String[] estadoCivil = {"Casada","Solteira","Uniao de Facto","Divorciada","Viuva"};
    private final static String[] subsistemas = {"ADSE","ADSE","ADSE","ADSE","ADSE","ADSE","ADM","SAD/GNR","SAD/PSP","SSMJ"};
    private final static String[] grupoSanguineo = {"A Rh +","A Rh -","B Rh +","B Rh -","AB Rh +","AB Rh -","O Rh +","O Rh -"};
    private final static String[] antecedentes = {"cancro","defeciencias","problema cardiaco","incontinencia","colesterol"};
    private final static String[] alergias = {"poeira","polen","animais","fungos"};
    private final static String[] cirurgias = {"adenoamigdalectomia","cirurgia nasal","timpanoplastia","mastoidectomia","microcirurgia de laringe"};
    private final static String[] medicamentos = {"Neosoro","Puran T4","Salonpas","Cliclo 21","Microvlar","Buscopan Composto","Rivotril","Dorflex","Glifage","Hipoglos"};
    private final static String[] contracetivos = {"preservativo","preservativo","preservativo","preservativo","preservativo","pilula","anel vaginal","diafragma","espermicida"};
    private final static String[] tiposAnalise = {"EcografiaGinecologica","EcografiaObstetrica","ECG","HSG","Espermograma","CMColo","RX","Mama","Exsudado","Urina"};
    private final static String[] tiposParto = {"eutocico","ventosa","forceps","cesariana","pelvico"};
    
    private static ArrayList<Integer> idosas = new ArrayList<>();
    private static ArrayList<Integer> criancas = new ArrayList<>();
    private static ArrayList<Integer> possiveisgravidas = new ArrayList<>();
    private static LinkedHashMap<Integer,Integer> gravidas = new LinkedHashMap<>();
    private static LinkedHashMap<Integer,String> reqAnalises = new LinkedHashMap<>();
    
    public static void main(String[] args){
        
        /*
            File
        */
        
        File SqlScript = new File("SQL_DataScript.sql");
        try {
            FileWriter scriptWriter = new FileWriter(SqlScript);
            
            scriptWriter.write("USE BD_Project;");
            scriptWriter.write("\n-- nota: colocar ';' em todos os inserts dos tipos de análise antes de executar\n");
            
            /*
                Insertion - Paciente
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Paciente VALUES \n");
            // attributes
            int nif=10000000, ss, sns;
            String nome, datanascim, resid, cp, contactos, mail, profissao, ec, subsistema;
            // insertions
            for(int i=0; i<iterNum[0]; i++) {
                // not null attributes
                nif = nif+1;
                nome = "paciente" + i;
                int anonascim = ThreadLocalRandom.current().nextInt(1930, 2004 + 1);
                if(anonascim < 1968) {
                    idosas.add(nif);
                } else if(anonascim > 2002) {
                    criancas.add(nif);
                } else {
                    possiveisgravidas.add(nif);
                }
                datanascim = anonascim + "-" + ThreadLocalRandom.current().nextInt(1, 12 + 1) + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                contactos = "9" + ThreadLocalRandom.current().nextInt(10000000, 69999999 + 1) + ", 9" + ThreadLocalRandom.current().nextInt(10000000, 69999999 + 1);
                mail = nome + "@myemail.pt";
                ss = 123456+i;
                sns = 654321+i;
                
                if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) { // if values that can be null aren't null:
                    // nullable attributes
                    resid = "rua " + alfabeto[ThreadLocalRandom.current().nextInt(0, 22 + 1)] + alfabeto[ThreadLocalRandom.current().nextInt(0, 22 + 1)] + alfabeto[ThreadLocalRandom.current().nextInt(0, 22 + 1)] + ", " + cidades[ThreadLocalRandom.current().nextInt(0, 5 + 1)] + ", Portugal";
                    cp = ThreadLocalRandom.current().nextInt(1000, 5000 + 1) + "-" + ThreadLocalRandom.current().nextInt(100, 999 + 1);
                    profissao = profissoes[ThreadLocalRandom.current().nextInt(0, 8 + 1)];
                    ec = estadoCivil[ThreadLocalRandom.current().nextInt(0, 4 + 1)];
                    subsistema = subsistemas[ThreadLocalRandom.current().nextInt(0, 9 + 1)];
                    
                    // writting on file
                    if(i == iterNum[0]-1) { // if last iteration
                        scriptWriter.write("(" + nif + " , '" + nome + "' , '" + datanascim + "' , '" + resid + "' , '" + cp + "' , '" + contactos +
                                "' , '" + mail + "' , '" + profissao + "' , '" + ec + "' , " + ss + " , " + sns + " , '" + subsistema + "'); \n");
                    } else {
                        scriptWriter.write("(" + nif + " , '" + nome + "' , '" + datanascim + "' , '" + resid + "' , '" + cp + "' , '" + contactos + 
                                "' , '" + mail + "' , '" + profissao + "' , '" + ec + "' , " + ss + " , " + sns + " , '" + subsistema + "'), \n");
                    }
                
                } else {
                    // writting on file
                    if(i == iterNum[0]-1) { // if last iteration
                        scriptWriter.write("(" + nif + " , '" + nome + "' , '" + datanascim + "' , " + null + " , " + null + " , '" + contactos + 
                                "' , '" + mail + "' , " + null + " , " + null + " , " + ss + " , " + sns + " , " + null + "); \n");
                    } else {
                        scriptWriter.write("(" + nif + " , '" + nome + "' , '" + datanascim + "' , " + null + " , " + null + " , '" + contactos + 
                                "' , '" + mail + "' , " + null + " , " + null + " , " + ss + " , " + sns + " , " + null + "), \n");
                    }
                }
            }
            scriptWriter.write("\n\n");
            
            /*
                Insertion - HistorialClinico
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.HistorialClinico VALUES \n");
            // attributes
            int nif2=10000000, ts, menarca, coitarca, gravidezes, partos, menopausa;
            String gs, antfam, antpess, cirur, alerg, tabaco, alcool, medicam, ciclos, contracecao, ho, dum, dpp;
            double altura, peso;
            // insertions
            for(int i=0; i<iterNum[0]; i++) {
                // not null attributes
                nif2 = nif2 + 1;
                gs = grupoSanguineo[ThreadLocalRandom.current().nextInt(0, 7 + 1)];
                altura = Math.round((1 + 0.1*ThreadLocalRandom.current().nextInt(0, 9 + 1) + 0.01*ThreadLocalRandom.current().nextInt(0, 9 + 1))*100.0)/100.0;
                peso = Math.round((ThreadLocalRandom.current().nextInt(40, 100 + 1) + 0.1*ThreadLocalRandom.current().nextInt(0, 9 + 1))*100.0)/100.0;
                ts = ThreadLocalRandom.current().nextInt(0, 1 + 1);
                
                if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) { // if values that can be null aren't null:
                    // nullable attributes
                    antfam = antpess = cirur = alerg = medicam = "";
                    int max = ThreadLocalRandom.current().nextInt(1, 5 + 1);
                    for(int j=0; j<max; j++) {
                        if(j==max-1) {
                            antfam = antfam + antecedentes[ThreadLocalRandom.current().nextInt(0, 4 + 1)];
                            antpess = antpess + antecedentes[ThreadLocalRandom.current().nextInt(0, 4 + 1)];
                        } else {
                            antfam = antfam + antecedentes[ThreadLocalRandom.current().nextInt(0, 4 + 1)] + ", ";
                            antpess = antpess + antecedentes[ThreadLocalRandom.current().nextInt(0, 4 + 1)] + ", ";
                        }
                    }
                    max = ThreadLocalRandom.current().nextInt(1, 3 + 1);
                    for(int k=0; k<max; k++) {
                        if(k==max-1) {
                            cirur = cirur + cirurgias[ThreadLocalRandom.current().nextInt(0, 4 + 1)];
                            alerg = alerg + alergias[ThreadLocalRandom.current().nextInt(0, 3 + 1)];
                        } else {
                            cirur = cirur + cirurgias[ThreadLocalRandom.current().nextInt(0, 4 + 1)] + ", ";
                            alerg = alerg + alergias[ThreadLocalRandom.current().nextInt(0, 3 + 1)] + ", ";
                        }
                    }
                    tabaco = ThreadLocalRandom.current().nextInt(1, 40 + 1) + " cigarros por dia";
                    alcool = ThreadLocalRandom.current().nextInt(1, 5 + 1) + "L por dia";
                    max = ThreadLocalRandom.current().nextInt(1, 10 + 1);
                    for(int l=0; l<max; l++) {
                        if(l==max-1) {
                            medicam = medicam + medicamentos[ThreadLocalRandom.current().nextInt(0, 9 + 1)];
                        } else {
                            medicam = medicam + medicamentos[ThreadLocalRandom.current().nextInt(0, 9 + 1)] + ", ";
                        }
                    }
                    
                    // writting on file according to pacient's age
                    if(criancas.contains(nif2)) {
                        
                        // writting on file
                        if(i == iterNum[0]-1) { // if last iteration
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + "); \n");
                        } else {
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + "), \n");
                        }
                        
                    } else if (idosas.contains(nif2)){
                        
                        // menarca, coitarca, contracecao, gravidezes, partos, ho, menopausa
                        menarca = ThreadLocalRandom.current().nextInt(12, 15 + 1);
                        coitarca = ThreadLocalRandom.current().nextInt(14, 30 + 1);
                        contracecao = "";
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                            contracecao = contracetivos[ThreadLocalRandom.current().nextInt(0, 8 + 1)];
                        }
                        menopausa = ThreadLocalRandom.current().nextInt(50, 60 + 1);
                        gravidezes = partos = 0;
                        ho = "";
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) { 
                            gravidezes = ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1);
                            if(gravidezes > 0) {
                                partos = ThreadLocalRandom.current().nextInt(0, gravidezes + 1);
                                ho = ThreadLocalRandom.current().nextInt(0, gravidezes + 1) + " gravidezes de risco, com " + (gravidezes-partos) + " abortos";
                            }
                        }
                        // writting on file
                        if(i == iterNum[0]-1) { // if last iteration
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , " + null + " , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , " + null + " , " + null + " , " + menopausa + "); \n");
                        } else {
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , " + null + " , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , " + null + " , " + null + " , " + menopausa + "), \n");
                        }
                        
                    } else {
                        
                        // menarca, coitarca, contracecao, gravidezes, partos, ho, dum
                        menarca = ThreadLocalRandom.current().nextInt(12, 15 + 1);
                        coitarca = ThreadLocalRandom.current().nextInt(14, 30 + 1);
                        contracecao = "";
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                            contracecao = contracetivos[ThreadLocalRandom.current().nextInt(0, 8 + 1)];
                        }
                        gravidezes = partos = 0;
                        ho = "";
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) { 
                            gravidezes = ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1) + ThreadLocalRandom.current().nextInt(0, 1 + 1);
                            if(gravidezes > 0) {
                                partos = ThreadLocalRandom.current().nextInt(0, gravidezes + 1);
                                if(partos < gravidezes && ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) { // dpp
                                    gravidas.put(nif2,gravidezes);
                                    ho = ThreadLocalRandom.current().nextInt(0, gravidezes + 1) + " gravidezes de risco, com " + (gravidezes-partos-1) + " abortos";
                                    int mes = ThreadLocalRandom.current().nextInt(1, 3 + 1);
                                    dum = 2018 + "-" + mes + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                                    dpp = 2018 + "-" + (mes+9) + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                                    // writting on file
                                    if(i == iterNum[0]-1) { // if last iteration
                                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , " + null + " , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , '" + dum + "' , '" + dpp + "' , " + null + "); \n");
                                    } else {
                                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , " + null + " , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , '" + dum + "' , '" + dpp + "' , " + null + "), \n");
                                    }
                                    continue;
                                    
                                } else {    // ciclos
                                    ho = ThreadLocalRandom.current().nextInt(0, gravidezes + 1) + " gravidezes de risco, com zero abortos";
                                    ciclos = ThreadLocalRandom.current().nextInt(25, 35 + 1) + " dias";
                                    dum = 2018 + "-" + 5 + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                                    
                                    // writting on file
                                    if(i == iterNum[0]-1) { // if last iteration
                                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , '" + ciclos + "' , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , '" + dum + "' , " + null + " , " + null + "); \n");
                                    } else {
                                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , '" + ciclos + "' , '" + contracecao + "' , " + gravidezes + " , " + partos + " , '" + ho + "' , '" + dum + "' , " + null + " , " + null + "), \n");
                                    }
                                    continue;
                                }
                            }
                        } // ciclos, dum
                        ciclos = ThreadLocalRandom.current().nextInt(25, 35 + 1) + " dias";
                        dum = 2018 + "-" + 5 + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                        // writting on file
                        if(i == iterNum[0]-1) { // if last iteration
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , '" + ciclos + "' , '" + contracecao + "' , " + null + " , " + null + " , " + null + " , '" + dum + "' , " + null + " , " + null + "); \n");
                        } else {
                            scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , '" + antfam + "' , '" + antpess + "' , '" + cirur + "' , '" + alerg + "' , " + ts + " , '" + tabaco + "' , '" + alcool + "' , '" + medicam + "' , " + menarca + " , " + coitarca + " , '" + ciclos + "' , '" + contracecao + "' , " + null + " , " + null + " , " + null + " , '" + dum + "' , " + null + " , " + null + "), \n");
                        }
                    }
                } else {
                    // writting on file
                    if(i == iterNum[0]-1) { // if last iteration
                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , " + null + " , " + null + " , " + null + " , " + null + " , " + ts + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + "); \n");
                    } else {
                        scriptWriter.write("(" + nif2 + " , '" + gs + "' , " + altura + " , " + peso + " , " + null +" , " + null + " , " + null + " , " + null + " , " + ts + " , " + null + " , " + null + " , " + null + " , "+ null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + " , " + null + "), \n");
                    }
                }
            }
            scriptWriter.write("\n\n");
            
            /*
                Insertion - Gravidez
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Gravidez VALUES \n");
            // attributes
            int nif3, ng;
            // insertions
            for(int i=0; i<gravidas.size(); i++) {
                nif3 = (int)gravidas.keySet().toArray()[i];
                ng = (int)gravidas.values().toArray()[i];
                
                // writting on file
                if(i == gravidas.size()-1) { // if last iteration
                    scriptWriter.write("(" + nif3 + " , '" + ng + "' , " + ThreadLocalRandom.current().nextInt(1, 5 + 1) + "); \n");
                } else {
                    scriptWriter.write("(" + nif3 + " , '" + ng + "' , " + ThreadLocalRandom.current().nextInt(1, 5 + 1) + "), \n");
                }
            }
            scriptWriter.write("\n\n");

            /*
                Insertion - Medico
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Medico VALUES \n"
                    + "( 11111111 , 'Joao Alegria' , 'joao.alegria@myemail.pt' , 'Ginecologia e Obstetricia' , "
                    + "40 , '2000-1-1' , 2500 ), "
                    + "( 22222222 , 'Filipe Pires' , 'filipe.pires@myemail.pt' , 'Ginecologia' , "
                    + "40 , '2000-1-1' , 2500 );");
            scriptWriter.write("\n\n");

            scriptWriter.write("\n\n");

            /*
                Insertion - Consulta
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Consulta VALUES \n");
            Map <Integer, Integer> consultas = new HashMap();
            Map <Integer, Integer> consultasInv = new HashMap();
            for(int con=0; con<iterNum[1];con++){
            	int nifCon=0;
            	String dataConsulta;
            	switch(ThreadLocalRandom.current().nextInt(1, 4 + 1)){
                    case 1:
                        nifCon=idosas.get(ThreadLocalRandom.current().nextInt(1, idosas.size()));
                        break;
                    case 2:
                        nifCon=criancas.get(ThreadLocalRandom.current().nextInt(1, criancas.size()));
                        break;
                    case 3:
                        nifCon=possiveisgravidas.get(ThreadLocalRandom.current().nextInt(1, possiveisgravidas.size()));
                        break;
                    case 4:
                        Object[] grav = gravidas.keySet().toArray();
                        nifCon=(Integer)grav[ThreadLocalRandom.current().nextInt(1, grav.length)];
                        break;
            	}

            	dataConsulta = ThreadLocalRandom.current().nextInt(2000, 2018 + 1)  + "-" + ThreadLocalRandom.current().nextInt(1, 12 + 1) + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
            	if(con==iterNum[1]-1){
            		consultas.put(nifCon, (con+1));
            		consultasInv.put((con+1), nifCon);
                    scriptWriter.write("(" + (con+1) + ",'" + dataConsulta + "',"+ThreadLocalRandom.current().nextInt(9, 20 + 1)+","+nifCon+");");
                    break;
            	}
            	consultas.put(nifCon, (con+1));
            	consultasInv.put((con+1), nifCon);
            	scriptWriter.write("(" + (con+1) + ",'" + dataConsulta + "',"+ThreadLocalRandom.current().nextInt(9, 20 + 1)+","+nifCon+"),\n");
            }
            scriptWriter.write("\n\n");
            
            /*
                Insertion - ConsultaObstetricia
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.ConsultaObstetricia VALUES \n");
            ArrayList<Integer> idobs= new ArrayList();
            Object[] grav = gravidas.keySet().toArray();
            String[] types = {"Normal","Normal","Normal","Normal","Normal","Normal","Normal","Normal","Normal", "Parto"};
            for(int con=0; con<gravidas.size();con++){
            	int z=gravidas.get((Integer)grav[con]);
            	int afu=ThreadLocalRandom.current().nextInt(0, 10 + 1), maf=ThreadLocalRandom.current().nextInt(0, 3 + 1), foco=ThreadLocalRandom.current().nextInt(0, 3 + 1); 
            	int ta=ThreadLocalRandom.current().nextInt(0, 1 + 1), exgin=ThreadLocalRandom.current().nextInt(0, 1 + 1), queixas=ThreadLocalRandom.current().nextInt(0, 1 + 1), medicacao=ThreadLocalRandom.current().nextInt(0, 1 + 1), obs=ThreadLocalRandom.current().nextInt(0, 1 + 1);
            	int nifaux=(Integer)grav[con];
            	if(con==gravidas.size()-1){
            		idobs.add(consultas.get(nifaux));
            		if(consultas.get(nifaux)==null){
            			continue;
            		}
                    scriptWriter.write("("+consultas.get(nifaux)+","+nifaux+","+z+",'"+types[ThreadLocalRandom.current().nextInt(0, 9 + 1)]+"',"+((afu==0)?"null":afu)+","+((maf==3)?"null":maf)+","+((foco==3)?"null":foco)+","+ThreadLocalRandom.current().nextInt(40, 100 + 1)+","+((ta==0)?"null":"'Lorem ipsum.'")+","+((exgin==0)?"null":"'Lorem ipsum.'")+","+((queixas==0)?"null":"'Lorem ipsum.'")+","+((medicacao==0)?"null":"'"+medicamentos[ThreadLocalRandom.current().nextInt(0, medicamentos.length)]+"'")+","+((obs==0)?"null":"'Lorem ipsum.'")+","+"null"+","+"null"+","+"null"+");");
                    break;
            	}
            	if(consultas.get(nifaux)==null){
            			continue;
            		}
            	idobs.add(consultas.get(nifaux));
            	scriptWriter.write("("+consultas.get(nifaux)+","+nifaux+","+z+",'"+types[ThreadLocalRandom.current().nextInt(0, 9 + 1)]+"',"+((afu==0)?"null":afu)+","+((maf==3)?"null":maf)+","+((foco==3)?"null":foco)+","+ThreadLocalRandom.current().nextInt(40, 100 + 1)+","+((ta==0)?"null":"'Lorem ipsum.'")+","+((exgin==0)?"null":"'Lorem ipsum.'")+","+((queixas==0)?"null":"'Lorem ipsum.'")+","+((medicacao==0)?"null":"'"+medicamentos[ThreadLocalRandom.current().nextInt(0, medicamentos.length)]+"'")+","+((obs==0)?"null":"'Lorem ipsum.'")+","+"null"+","+"null"+","+"null"+"),\n");

            }
            scriptWriter.write("\n\n");

            /*
                Insertion - ConsultaGinecologia
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.ConsultaGinecologia VALUES \n");
            ArrayList<Integer> idgin = new ArrayList();
            int aux=0;
            for(int con=0; con<iterNum[1];con++){
            	int idg;
            	if(aux==iterNum[1]-1){
                    break;
            	}
            	do{
                    aux=aux+1;
                    idg=aux;
            	}while(idobs.contains(idg));
            	if(con==iterNum[1]-idobs.size()-1){
                    scriptWriter.write("(" +idg+","+"'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam aliquam arcu id nisi maximus, sit amet scelerisque velit eleifend. Aenean sed tincidunt mi, a consequat orci. Curabitur consectetur rhoncus justo, vel interdum enim mattis non. Ut iaculis tincidunt ipsum, at imperdiet nibh elementum id. Maecenas vitae neque vel dui ultrices dictum ut eget quam. '"+");");
                    break;
            	}
            	scriptWriter.write("(" +idg+","+"'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam aliquam arcu id nisi maximus, sit amet scelerisque velit eleifend. Aenean sed tincidunt mi, a consequat orci. Curabitur consectetur rhoncus justo, vel interdum enim mattis non. Ut iaculis tincidunt ipsum, at imperdiet nibh elementum id. Maecenas vitae neque vel dui ultrices dictum ut eget quam.'"+"),\n");
            }
            scriptWriter.write("\n\n");
            
            /*
                Insertion - Dirige
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Dirige VALUES \n");
            // attributes
            int medico = 11111111, consulta;
            // insertions
            for(int con=0; con<iterNum[1];con++){
                medico = 11111111;
                consulta = con+1;
                if(con==iterNum[1]-1) {
                    scriptWriter.write("( " + medico + " , " + consulta + " ); \n");
                } else {
                    scriptWriter.write("( " + medico + " , " + consulta + " ), \n");
                }
            }
            
            /*
                Insertion - Bebe
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.Bebe VALUES \n");
            // attributes
            int nifmae, ngravidez,bebe = 1;
            double pesobebe;
            String parto, indicacao, sexobebe, apgar; 
            // insertions
            for(int i=0; i<gravidas.size(); i++) {
                nifmae = (int)gravidas.keySet().toArray()[i];
                ngravidez = (int)gravidas.values().toArray()[i];
                parto = tiposParto[ThreadLocalRandom.current().nextInt(0, 4 + 1)];
                if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                    indicacao = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                        + "Nulla suscipit orci vitae erat tristique viverra. "
                        + "Vivamus pulvinar quam luctus ornare ullamcorper. "
                        + "Nam blandit, dolor sed pretium mollis, lorem metus tempus lorem, vitae porta erat nulla at metus. "
                        + "Phasellus id magna dignissim, ullamcorper mauris vitae, egestas arcu. "
                        + "Vestibulum ultrices lectus quis erat luctus malesuada a ac dui. "
                        + "Nam facilisis scelerisque feugiat. "
                        + "Duis ultrices justo vitae magna sagittis gravida.";
                } else {
                    indicacao = "";
                }
                if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                    sexobebe = "Masculino";
                } else {
                    sexobebe = "Feminino";
                }
                pesobebe = Math.round((ThreadLocalRandom.current().nextInt(1, 3 + 1) + 0.1*ThreadLocalRandom.current().nextInt(0, 9 + 1))*100.0)/100.0;;
                apgar = "normal";
                if(i==gravidas.size()-1) {
                    scriptWriter.write("( " + nifmae + " , " + ngravidez + " , " + bebe + " , '" + parto + "' , '" + indicacao + "' , '" + sexobebe + "' , " + pesobebe + " , '" + apgar + "' ); \n");
                } else {
                    scriptWriter.write("( " + nifmae + " , " + ngravidez + " , " + bebe + " , '" + parto + "' , '" + indicacao + "' , '" + sexobebe + "' , " + pesobebe + " , '" + apgar + "' ), \n");
                }
                
            }
            
            /*
                Insertion - RequisicaoAnalise
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.RequisicaoAnalise VALUES \n");
            // attributes
            int idra = 1, nifm = 11111111;
            String datara = "", tipo = "";
            int nifp = 10000000;
            // insertions
            for(int i=0; i<iterNum[2]; i++) {
                idra = idra + 1;
                nifp = nifp + 1;
                datara = 2018 + "-" + ThreadLocalRandom.current().nextInt(1, 4 + 1) + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                tipo = tiposAnalise[ThreadLocalRandom.current().nextInt(0, 9 + 1)];
                reqAnalises.put(idra, tipo);
                // writting on file
                if(i == iterNum[2]-1) { // if last iteration
                    scriptWriter.write("(" + idra + " , '" + datara + "' , " + nifp + " , " + nifm + " , '" + tipo + "'); \n");
                } else {
                    scriptWriter.write("(" + idra + " , '" + datara + "' , " + nifp + " , " + nifm + " , '" + tipo + "'), \n");
                }
            }
            scriptWriter.write("\n\n");
            
            /*
                Insertion - DescricaoAnalise
            */
            
            scriptWriter.write("\nINSERT INTO ProjetoConsultorio.DescricaoAnalise VALUES \n");
            // attributes
            int idda = 1;
            int nconsulta = 2, req;
            String datada, descricao; 
            // insertions
            String[] analysisInsertions = new String[10];
            for(int j=0; j<analysisInsertions.length; j++) {
                analysisInsertions[j] = "\nINSERT INTO ProjetoConsultorio." + tiposAnalise[j] + " VALUES \n";
            }
            for(int i=0; i<iterNum[2]; i++) {
                idda = idda + 1;
                datada = 2018 + "-" + ThreadLocalRandom.current().nextInt(5, 5 + 1) + "-" + ThreadLocalRandom.current().nextInt(1, 28 + 1);
                descricao = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                        + "Nulla suscipit orci vitae erat tristique viverra. "
                        + "Vivamus pulvinar quam luctus ornare ullamcorper. "
                        + "Nam blandit, dolor sed pretium mollis, lorem metus tempus lorem, vitae porta erat nulla at metus. "
                        + "Phasellus id magna dignissim, ullamcorper mauris vitae, egestas arcu. "
                        + "Vestibulum ultrices lectus quis erat luctus malesuada a ac dui. "
                        + "Nam facilisis scelerisque feugiat. "
                        + "Duis ultrices justo vitae magna sagittis gravida.";
                req = (int)reqAnalises.keySet().toArray()[i];
                
                // writting on file
                if(i == iterNum[2]-1) { // if last iteration
                    scriptWriter.write("(" + idda + " , " + (nconsulta+i) + " , " + req + " , '" + datada + "' , '" + descricao + "'); \n");
                } else {
                    scriptWriter.write("(" + idda + " , " + (nconsulta+i) + " , " + req + " , '" + datada + "' , '" + descricao + "'), \n");
                }
                
                switch((String)reqAnalises.values().toArray()[i]) {
                    case "EcografiaGinecologica":
                        analysisInsertions[0] = analysisInsertions[0] + "(" + idda + "), ";
                        break;
                    case "EcografiaObstetrica":
                        int semanas = ThreadLocalRandom.current().nextInt(1, 39 + 1);
                        int dias = ThreadLocalRandom.current().nextInt(1, 39 + 1);
                        analysisInsertions[1] = analysisInsertions[1] + "(" + idda + " , " + semanas + " , " + dias + "), ";
                        break;
                    case "ECG":
                        analysisInsertions[2] = analysisInsertions[2] + "(" + idda + "), ";
                        break;
                    case "HSG":
                        analysisInsertions[3] = analysisInsertions[3] + "(" + idda + "), ";
                        break;
                    case "Espermograma":
                        analysisInsertions[4] = analysisInsertions[4] + "(" + idda + "), ";
                        break;
                    case "CMColo":
                        String convencional, meioliquido;
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                            convencional = "Convencional";
                            meioliquido = "";
                        } else {
                            convencional = "";
                            meioliquido = "Meio Liquido";
                        }
                        analysisInsertions[5] = analysisInsertions[5] + "(" + idda + " , '" + convencional + "' , '" + meioliquido + "'), ";
                        break;
                    case "RX":
                        String rxtipo = "torax";
                        analysisInsertions[6] = analysisInsertions[6] + "(" + idda + " , '" + rxtipo + "'), ";
                        break;
                    case "Mama":
                        String mamo, ecomam;
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                            mamo = "Mamografia"; ecomam = "";
                        } else {
                            mamo = ""; ecomam = "Ecografia Mamaria";
                        }
                        analysisInsertions[7] = analysisInsertions[7] + "(" + idda + " , '" + mamo + "' , '" + ecomam + "'), ";
                        break;
                    case "Exsudado":
                        String vb, vm, vp;
                        if(ThreadLocalRandom.current().nextInt(0, 2 + 1) == 1) {
                            vb = "Vaginal Bacteriologico"; vm = ""; vp = "";
                        } else if (ThreadLocalRandom.current().nextInt(0, 2 + 1) == 2) {
                            vb = ""; vm = "Vaginal Micologico"; vp = "";
                        } else {
                            vb = ""; vm = ""; vp = "Vaginal Parasitologico";
                        }
                        int vr = ThreadLocalRandom.current().nextInt(0, 1 + 1);
                        analysisInsertions[8] = analysisInsertions[8] + "(" + idda + " , '" + vb + "' , '" + vm + "' , '" + vp + "' , " + vr + "), ";
                        break;
                    case "Urina":
                        int tig = ThreadLocalRandom.current().nextInt(0, 1 + 1);
                        if(ThreadLocalRandom.current().nextInt(0, 1 + 1) == 1) {
                            String sum = "Sumaria Urinas";
                            int uroc = 0;
                            analysisInsertions[9] = analysisInsertions[9] + "(" + idda + " , '" + sum + "' , " + uroc + " , " + null + " , " + tig + "), ";
                        } else {
                            int uroc = 1;
                            String valoruroc = ThreadLocalRandom.current().nextInt(0, 10 + 1) + " unidades";
                            analysisInsertions[9] = analysisInsertions[9] + "(" + idda + " , " + null + " , " + uroc + " , '" + valoruroc + "' , " + tig + "), ";
                        }
                        break;
                }
            }
            // writting on file
            for(int j=0; j<analysisInsertions.length; j++) {
                scriptWriter.write(analysisInsertions[j]);
            }
            scriptWriter.write("\n\n");
            scriptWriter.close();
        } catch (IOException ex) {
            Logger.getLogger(Insertions.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
