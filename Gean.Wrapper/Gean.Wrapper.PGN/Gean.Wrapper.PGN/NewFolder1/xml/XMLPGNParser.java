/*
 * XMLPGNParser.java
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
package com.supareno.pgnparser.xml;

import java.util.ArrayList;
import java.util.List;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.MalformedURLException;
import java.net.URL;

import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLStreamConstants;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;

import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.xml.DOMConfigurator;

import com.supareno.pgnparser.*;

/**
 * The <code>XMLPGNParser</code> class 
 * 
 * @author supareno
 *
 */
public class XMLPGNParser {
	
	static final private Logger LOGGER=Logger.getLogger(XMLPGNParser.class);
	// Default Logger level
	static final private Level DEFAULT_LOGGER_LEVEL=Level.DEBUG;
	// Logger level
	private Level _loggerLevel=DEFAULT_LOGGER_LEVEL;

	/**
	 * Creates a new <code>XMLPGNParser</code> with a Logger <code>Level</code>
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
	public XMLPGNParser(Level loggerLevel){
		this(loggerLevel,"");
	}
	
	/**
	 * Creates a new <code>XMLPGNParser</code> with a Logger <code>Level</code>
	 * 
	 * @param loggerLevel the <code>Level</code> of the Logger. It could be:<br>
	 * <ul><li>{@link Level#ALL}</li>
	 * <li>{@link Level#DEBUG}</li>
	 * <li>{@link Level#ERROR}</li>
	 * <li>{@link Level#WARN}</li>
	 * <li>{@link Level#FATAL}</li>
	 * <li>{@link Level#INFO}</li>
	 * <li>{@link Level#OFF}</li></ul>
	 * @param xmlConfigurationFile the xml configuration file used to configure the Logger.
	 * 
	 * @see Logger
	 * @see Level
	 * @see #setLoggerConfiguratorFile(String)
	 */
	public XMLPGNParser(Level loggerLevel, String xmlConfigurationFile){
		if(loggerLevel!=null){
			_loggerLevel=loggerLevel;
		}
		LOGGER.setLevel(_loggerLevel);
		setLoggerConfiguratorFile(xmlConfigurationFile);
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
	 * Parses an xml file and returns a List of <code>PGNGame</code>s.
	 * @param file a String representation of the file to parse
	 * @return a List of <code>PGNGame</code>s
	 */
	public List<PGNGame> parseXMLFile(String file){
		return parseXMLFile(new File(file));
	}
	
	/**
	 * Parses an xml file and returns a List of <code>PGNGame</code>s
	 * @param file the xml file to parse
	 * @return a List of <code>PGNGame</code>s or <code>null</code> if the file
	 * cannot be found.
	 */
	public List<PGNGame> parseXMLFile(File file){
		Reader r=null;
		try {
			r=new FileReader(file);
		} catch (FileNotFoundException e) {
			log("error in parseXMLFile ",e);
		}
		return parseXMLFile(r);
	}
	
	/**
	 * Parses an xml file located on the www.
	 * @param url the String representation of the file to parse
	 * @return  a List of <code>PGNGame</code>s or <code>null</code> if the url
	 * is malformed.
	 */
	public List<PGNGame> parseXMLURL(String url){
		try {
			URL u=new URL(url);
			return parseXMLURL(u);
		} catch (MalformedURLException e) {
			log("error in parseXMLFile ",e);
			return null;
		}		
	}
	
	/**
	 * Parses an xml file located on the www.
	 * @param url the url of the file to parse
	 * @return a List of <code>PGNGame</code>s or <code>null</code> if an
	 * IOException occurs.
	 */
	public List<PGNGame> parseXMLURL(URL url){
		List<PGNGame> games=null;
		try {
			games=parseXMLFile(new InputStreamReader(url.openStream()));
		} catch (IOException e) {
			log("error in parseXMLURL ",e);
		}
		return games;
	}
	
	/**
	 * Parses an xml file form a java.io.Reader
	 * @param reader the stream that contains the xml file 
	 * @return a List of <code>PGNGame</code>s extract from the stream or <code>null</code> if
	 * an error occurs.
	 */
	@SuppressWarnings("unused")
	public List<PGNGame> parseXMLFile(Reader reader){
		if(reader==null){
			return null;
		}
		List<PGNGame> games=null;
		XMLInputFactory factory = XMLInputFactory.newInstance();
		try {
			XMLStreamReader parser = factory.createXMLStreamReader(reader);
			PGNGame game=null;
			PGNHit hit=null;
			boolean hitparsed=false;
			for (int event = parser.next();  
		       event != XMLStreamConstants.END_DOCUMENT;
		       event = parser.next()) {
				switch(event){
				case XMLStreamConstants.START_ELEMENT:
					String s=parser.getLocalName();
					if(s.equals("games")){
						games=new ArrayList<PGNGame>();
						hitparsed=false;
					}else if(s.equals("game")){
						game=new PGNGame();						
						hitparsed=false;
					}else if(s.equals("game_property")){
						if(parser.getAttributeCount()==2){
							treateGameAttribute(game, parser.getAttributeValue(0),parser.getAttributeValue(1));
						}			
						hitparsed=false;
					}else if(s.equals("hit")){
						hitparsed=true;
						hit=new PGNHit();
					}
					break;
				case XMLStreamConstants.CHARACTERS:
					if(hit!=null && hitparsed && parser.getText()!=null && parser.getText().length()>0){
						hit.setHit(parser.getText());
					}
					break;
//				case XMLStreamConstants.CDATA:
//					if(hitparsed && parser.getText()!=null){
//						hit.setHit(parser.getText());
//					}
//					break;
				case XMLStreamConstants.END_ELEMENT:
					String end=parser.getLocalName();
					if(end.equals("game")){
						if(games!=null){
							games.add(game);
							game=null;
						}
					}else if(end.equals("hit")){
						if(game!=null && hit!=null){
							game.addPGNHit(hit);
						}
						hit=null;
					}
					break;
				default:
					log("element unknown :-(");
					break;
				}
			}
		} catch (XMLStreamException e) {
			log("error in parseXMLFile ",e);
		}
		return games;
	}
	
	/**
	 * Writes an xml file to a PGN file and returns <code>true</code> if succeeded,
	 * <code>false</code> otherwise.
	 * 
	 * @param xmlFile the name of the xml file to retreive the datas
	 * @param pgnFilename the name of the file to write without any extension. The 
	 * <code>pgn</code> extension will be added by the writer.
	 * 
	 * @return <code>true</code> if succeeded, <code>false</code> otherwise.
	 */
	public boolean writeXMLToPGN(String xmlFile, String pgnFilename){
		String fname=pgnFilename;
		if(pgnFilename.indexOf(".")>0){
			fname=pgnFilename.substring(0, pgnFilename.indexOf("."));
		}
		List<PGNGame> games=parseXMLFile(xmlFile);
		if(games==null){
			log("nothing to write to the PGN file : xml parse returns any game");
			return false;
		}
		try {
			PGNWriter.writePGNFile(games, fname);
	        return true;
	    } catch (IOException e) {
	    	log("IOException in the XMLtoPGN writing ",e);
	    } catch (NullPointerException e) {
	    	log("NullPointerException in the XMLtoPGN writing ",e);
	    }
		return false;
	}
	/**
	 * Fills the <code>game</code> with the specified <code>attribute</code> and
	 * <code>value</code> if the attribute is associated to a method.
	 * @param game the current game
	 * @param attribute the name of the attribute
	 * @param value the value of the attribute
	 */
	private void treateGameAttribute(PGNGame game, String attribute, String value){
		if(game==null){return;}
		game.addAttributeNValue(attribute, value);
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
