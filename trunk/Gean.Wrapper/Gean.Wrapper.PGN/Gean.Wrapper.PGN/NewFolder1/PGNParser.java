/*
 * PGNParser.java
 * 
 * Copyright 2008 supareno 
 *  
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at
 *  
 *  	http://www.apache.org/licenses/LICENSE-2.0 
 *  
 * Unless required by applicable law or agreed to in writing, software 
 * distributed under the License is distributed on an "AS IS" BASIS, 
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
 * See the License for the specific language governing permissions and 
 * limitations under the License.
 */
package com.supareno.pgnparser;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileFilter;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.log4j.Logger;
import org.apache.log4j.Level;
import org.apache.log4j.xml.DOMConfigurator;

/**
 * The <code>PGNParser</code> class is the class used to parse the PGN files.
 * These files could be a <code>String</code> or a <code>File</code>.<br>
 * By default, the parser stores only the black winning and draws games. Use the
 * {@link #setOnlyBlackWins(boolean)  setOnlyBlackWins} method to change the 
 * value of the <code>setOnlyBlackWins</code> parameter.
 * 
 * @author supareno
 *
 * @see PGNGame
 * @see PGNHit
 */
public class PGNParser {

	// private Logger 
	static final private Logger LOGGER=Logger.getLogger(PGNParser.class);
	static final private Level DEFAULT_LOGGER_LEVEL=Level.DEBUG;
	// game separator used in the first parse.
	static final private String GAME_SEPARATOR="###";
	/*
	 * this boolean is used during the parsing. If set to true, it will store 
	 * only the black winning and the draw games
	 */
	private boolean onlyBlackwins=true;
	// Logger level
	private Level _loggerLevel=DEFAULT_LOGGER_LEVEL;
	
	/**
	 * The String representation of the pattern used to match the attributes of
	 * the PGN file.
	 * @see #ATTRIBUTES_PATTERN
	 */
	static final public String ATTRIBUTES_STRING_PATTERN="\\[[^\\[]*\\]";
	
	/**
	 * The String representation of the pattern used to match the hits of
	 * the PGN file.<br>
	 * This pattern is composed in two parts: <br>
	 * The first one is for the hit number<br>
	 * <code>[0-9]+[.]</code><br> 
	 * The second one is for the hit (composed in two same part seperate with a space)<br>
	 * <code>([a-zA-Z]*[0-9][\\+]?|[O]+[\\-][O]+[\\+]?)[ ]([a-zA-Z]*[0-9][\\+]?|[O]+[\\-][O]+[\\+]?)</code>
	 * 
	 * @see #HITS_PATTERN
	 */
	static final public String HITS_STRING_PATTERN="[0-9]+[.]" +
			"([a-zA-Z]*[0-9][\\+]?|[O]+[\\-][O]+[\\+]?)[ ]([a-zA-Z]*[0-9][\\+]?|[O]+[\\-][O]+[\\+]?)";
	/**
	 * The String representation of the pattern used to check a number validity.
	 * @see #NUMBER_VALIDITY_PATTERN
	 */
	static final private String NUMBER_VALIDITY_STRING_PATTERN="[0-9]+";
	
	/**
	 * <code>Pattern</code> used to parse the attributes of the PGN file. It compiles
	 * with the {@link #ATTRIBUTES_STRING_PATTERN} pattern. 	 * 
	 * @see Pattern
	 */
	static final public Pattern ATTRIBUTES_PATTERN=Pattern.compile(ATTRIBUTES_STRING_PATTERN);
	/**
	 * <code>Pattern</code> used to parse the hits of the PGN file. It compiles
	 * with the {@link #HITS_STRING_PATTERN} pattern. 
	 * @see Pattern
	 */
	static final public Pattern HITS_PATTERN=Pattern.compile(HITS_STRING_PATTERN);
	/**
	 * <code>Pattern</code> used to check the validity of a Number. It compiles
	 * with the {@link #NUMBER_VALIDITY_STRING_PATTERN} pattern.
	 * @see Pattern
	 */
	static final public Pattern NUMBER_VALIDITY_PATTERN=Pattern.compile(NUMBER_VALIDITY_STRING_PATTERN); 
	
	/**
	 * Creates a PGNParser with a Level for the Logger. This Logger will log nothing 
	 * if the logger xml configuration file is not set!. 
	 * 
	 * @param loggerLevel the <code>Level</code> of the Logger. It could be:<br>
	 * <ul><li>{@link Level#ALL}</li>
	 * <li>{@link Level#DEBUG}</li>
	 * <li>{@link Level#ERROR}</li>
	 * <li>{@link Level#WARN}</li>
	 * <li>{@link Level#FATAL}</li>
	 * <li>{@link Level#INFO}</li>
	 * <li>{@link Level#OFF}</li></ul>
	 * 
	 * @see Logger
	 * @see Level
	 */
	public PGNParser(Level loggerLevel){
		this(loggerLevel,"");
	}
	
	/**
	 * Creates a PGNParser with a Level for the Logger and a configuration file 
	 * for Log4j.
	 * 
	 * @param loggerLevel the <code>Level</code> of the Logger. It could be:<br>
	 * <ul><li>{@link Level#ALL}</li>
	 * <li>{@link Level#DEBUG}</li>
	 * <li>{@link Level#ERROR}</li>
	 * <li>{@link Level#WARN}</li>
	 * <li>{@link Level#FATAL}</li>
	 * <li>{@link Level#INFO}</li>
	 * <li>{@link Level#OFF}</li></ul>
	 * @param log4jXmlConfigFile the xml log4j configuration file
	 * 
	 * @see Logger
	 * @see Level
	 */
	public PGNParser(Level loggerLevel, String log4jXmlConfigFile){
		if(loggerLevel!=null){
			_loggerLevel=loggerLevel;
		}
		LOGGER.setLevel(_loggerLevel);
		setLoggerConfiguratorFile(log4jXmlConfigFile);
	}
	
	/**
	 * Returns the Level of the logger
	 * @return the Level of the logger
	 * @see Level
	 * @see Logger
	 */
	public Level getLoggerLevel(){
		return this._loggerLevel;
	}
	
	/**
	 * Sets the <code>LOGGER</code> xml configuration file.
	 * @param file the log4j xml configuration file
	 * @see Logger
	 */
	public void setLoggerConfiguratorFile(String file){
		if(file.length()>0){
			DOMConfigurator.configure(file);
		}
	}
	
	/**
	 * Sets the <code>onlyBlackwins</code> value. If sets to <code>true</code>, 
	 * it will store only the black winning and the draw games , <code>false</code>
	 * otherwise.
	 * @param black  <code>true</code>, it will store only the black winning and
	 * the draw games , <code>false</code>
	 * otherwise.
	 */
	public void setOnlyBlackWins(boolean black){
		this.onlyBlackwins=black;
	}
	
	/**
	 * Returns the value of the <code>onlyBlackwins</code> parameter.
	 * @return the value of the <code>onlyBlackwins</code> parameter.
	 */
	public boolean getOnlyBlackWins(){return this.onlyBlackwins;}
	
	/**
	 * Parses the given folder. Only the <i>pgn</i> extension files are listed
	 * and parsed. Returns a List of List of <code>PGNGame</code>s
	 * 
	 * @param folder the folder where the PGN files are located
	 * 
	 * @return a List of List of <code>PGNGame</code>s
	 */
	public List<List<PGNGame>> parseFolder(String folder){
		List<List<PGNGame>> gamesList=null;
		File file=new File(folder);
		if(file.exists()){// if the folder exists, we list it and parse each file
			File[] files=file.listFiles(new FileFilter(){
				public boolean accept(File pathname) {
					if(pathname.getName().toLowerCase().endsWith(".pgn")){
						return true;
					}
					return false;
				}				
			});
			if(files!=null && files.length>0){
				gamesList=new ArrayList<List<PGNGame>>();
				for(File f:files){
					gamesList.add(parseFile(f));
				}
			}else{
				log("empty folder :-(");
			}
		}
		return gamesList;
	}
	
	/**
	 * Parses a PGN file and returns a List of the <code>PGNGame</code>s contents
	 * in the file. Returns <code>null</code> if a <code>FileNotFoundException</code>
	 * occurs.
	 * 
	 * @param file a String representation of the file to parse
	 * 
	 * @return a List of the <code>PGNGame</code>s contents in the file
	 */
	public List<PGNGame> parseFile(String file){
		return parseFile(new File(file));
	}
	
	/**
	 * Parses a PGN file and returns a List of the <code>PGNGame</code>s contents
	 * in the file. Returns <code>null</code> if a <code>FileNotFoundException</code>
	 * occurs.
	 * 
	 * @param file the File to parse
	 * 
	 * @return a List of the <code>PGNGame</code>s present in the file
	 */
	public List<PGNGame> parseFile(File file){
		String games=null;
		try {
			games=formatPGNFile(new FileReader(file));
		} catch (FileNotFoundException e) {
			log("FileNotFoundException: ",e);
		}
		if(games!=null && games.length()>0){
			return parseContents(games);
		}else{
			log("no game when file parsing :-(");
		}
		return null;
	}
	
	/**
	 * Parses a PGN file and returns a List of the <code>PGNGame</code>s contents
	 * in the file. Returns <code>null</code> if a <code>MalformedURLException</code>
	 * or an <code>IOException</code> occurs.
	 * 
	 * @param file the url to parse
	 * 
	 * @return a List of the <code>PGNGame</code>s contents in the file
	 */
	public List<PGNGame> parseURL(String url){
		String games=null;
		URL _url=null;
		try {
			_url=new URL(url);
			games=formatPGNFile(new InputStreamReader(_url.openStream()));
			if(games!=null && games.length()>0){
				return parseContents(games);
			}
		} catch (MalformedURLException e) {
			log("MalformedURLException: ",e);
		} catch (IOException e) {
			log("IOException: ",e);
		}
		return null;
	}
		
	/**
	 * This method parses a PGN file and formats it to be easely parseable by
	 * games.
	 * @param reader the Reader ...
	 * @return a String representation of the content of the file
	 */
	private String formatPGNFile(Reader reader){
		StringBuffer contents = new StringBuffer();
		String lastLine="no";
	    BufferedReader input = null;
	    try {
	      input = new BufferedReader(reader);
	      String line = null; 
	      while (( line = input.readLine()) != null){
	    	  if(line.startsWith("[") && !lastLine.endsWith("]")){
	    		  contents.append(GAME_SEPARATOR);
	    	  }
	          contents.append(line);
	          contents.append(System.getProperty("line.separator"));
	          lastLine=line;
	      }
	    }
	    catch (FileNotFoundException ex) {
	      log("error in formatting the PGN file",ex);
	    }
	    catch (IOException ex){
		   log("error in formatting the PGN file",ex);
	    }
	    finally {
	      try {
	        if (input!= null) {
	          //flush and close both "input" and its underlying Reader
	          input.close();
	        }
	      }
	      catch (IOException ex) {
		      log("error in formatting the PGN file",ex);
	      }
	    }
		return contents.toString();
	}
	
	/**
	 * Parses a PGN content in two steps. First step it parses the attributes (
	 * {@link #parseAttributes(PGNGame, String) attributes}) and the second step 
	 * is to parse the hits ({@link #parseHits(String) hits}. It returns a List
	 * of <code>PGNGame</code>.
	 * 
	 * @param content the PGN game String representation to parse
	 * 
	 * @return a List of <code>PGNGame</code>.
	 */
	private List<PGNGame> parseContents(String content) {
		List<PGNGame> games=new ArrayList<PGNGame>();
		String[] gamesString=content.split(GAME_SEPARATOR);
		for(String s:gamesString){
			String attributes=s.substring(0, s.lastIndexOf("]")+1);
			String hits=s.substring(s.lastIndexOf("]")+1, s.length()).trim();
			if(attributes.length()>0 && hits.length()>0){
				PGNGame pgn=treatePGNString(attributes, hits);
				if(pgn!=null){
					if(onlyBlackwins){
						// test if it's a black win
						if(pgn.getValue("Result")!=null){
							if(pgn.getValue("Result").equals("0-1")||
									pgn.getValue("Result").equals("1/2-1/2")){
								games.add(pgn);
							}
						}
					}else{
						games.add(pgn);
					}
				}
			}
		}		
		return games;
	}
	
	/**
	 * Parses the <code>attributes</code> and the <code>hits</code> and returns
	 * a <code>PGNGame</code> filled with the datas
	 * 
	 * @param attributes the attributes of the PGN game
	 * @param hits the hits of the PGN game
	 * 
	 * @return a <code>PGNGame</code> filled with the datas
	 */
	private PGNGame treatePGNString(String attributes, String hits){
		PGNGame p=new PGNGame();
		parseAttributes(p, attributes);
		parseHits(p, hits);
		return p;
	}
	
	/**
	 * Parses the PGN attributes. These attributes looks like this:<br>
	 * <pre>
	 * [Event "event_name"]
	 * [Site "site_name"]
	 * [Date "date"]
	 * [Round "round_number"]
	 * [White "player_name"]
	 * [Black "player_name"]
	 * [Result "result"]
	 * [WhiteElo "elo_number"]
	 * [BlackElo "elo_number"]
	 * [ECO "eco"]
	 * </pre>
	 * It uses the {@link #ATTRIBUTES_PATTERN} Pattern to parse the attributes.
	 * 
	 * @param pgn the <code>PGNGame</code> to fill
	 * @param attributes the String representation of the attributes to parse 
	 * 
	 * @return the <code>PGNGame</code> filled with the attributes found
	 * 
	 * @see #ATTRIBUTES_PATTERN
	 */
	private PGNGame parseAttributes(PGNGame pgn, String attributes){
		Matcher matcher = ATTRIBUTES_PATTERN.matcher(attributes);
		while(matcher.find()){
			String[] str=matcher.group().split("\"*\"");
			String s1=str[0].substring(1, str[0].length()).trim();
			String s2=str[1];
			pgn.addAttributeNValue(s1, s2);
		}
		return pgn;
	}
		
	/**
	 * Parses the PGN hits. Uses the {@link #HITS_PATTERN} Pattern to parse the hits. 
	 * @param pgn the <code>PGNGame</code> to fill
	 * @param hits the String representation of the hits to parse 
	 * @return the <code>PGNGame</code> filled with the attributes found 
	 * @see #HITS_PATTERN
	 */
	private PGNGame parseHits(PGNGame pgn, String hits){
		StringBuilder newHit=new StringBuilder();
		String[] strings=hits.split("\n");
		for(String s:strings){
			newHit.append(s);
		}				 		
		List<PGNHit> list=new ArrayList<PGNHit>();		
		Matcher matcher = HITS_PATTERN.matcher(newHit.toString());
		while(matcher.find()){
			String[] str=matcher.group().split("\\.");
			if(str.length<1){
				continue;
			}			
			list.add(new PGNHit(str[1]));
		}
		pgn.setPGNHits(list);
		return pgn;
	}
		
	/**
	 * Logs the message using the Logger with the Level set.
	 * @param msg the message to log
	 */
	public void log(String msg){
		log(msg, null);
	}
	
	/**
	 * Logs the message and the Throwable using the Logger with the Level set.
	 * @param msg the message to log
	 * @param t the Throwable to log
	 * @see Throwable
	 */
	public void log(String msg, Throwable t){
		if(LOGGER.isEnabledFor((Level)_loggerLevel)){
			if(t!=null){
				LOGGER.log(_loggerLevel, msg, t);
			}else{
				LOGGER.log(_loggerLevel, msg);
			}
		}
	}
}
