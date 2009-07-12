/*
 * XMLPGNWriter.java
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

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.text.SimpleDateFormat;
import java.util.*;

import javax.xml.stream.XMLOutputFactory;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamWriter;

import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.log4j.xml.DOMConfigurator;

import com.supareno.pgnparser.PGNGame;
import com.supareno.pgnparser.PGNHit;

/**
 * The <code>XMLPGNWriter</code> class is used to write to and xml file one
 * or more <code>PGNGame</code>s. This class is using the new XML api provided
 * with java 1.6.
 * 
 * @author supareno
 *
 */
public class XMLPGNWriter {

	static final private Logger LOGGER=Logger.getLogger(XMLPGNWriter.class);
	static final private Level DEFAULT_LOGGER_LEVEL=Level.DEBUG;
	// SimpleDateFormat used to format the date
	public static final SimpleDateFormat DATEFORMAT=new SimpleDateFormat("yyyyMMddHHmmss");
	// name of the file
	private String _filename=DEFAULT_FILE_NAME;
	// Logger level
	private Level _loggerLevel=DEFAULT_LOGGER_LEVEL;
	
	/**
	 * Default file name: sets to <code>pgnfile</code> with the date of the
	 * creation added after. The format of the date is:<br><code>yyyyMMddHHmmss</code><br> 
	 * The extension will be added automatically by the writer.
	 */
	static final public String DEFAULT_FILE_NAME="pgnfile";

	/**
	 * Empty constructor. Creates a new <code>XMLPGNWriter</code> with a 
	 * {@link #DEFAULT_FILE_NAME} and a Level for the Logger.
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
	 * @see #XMLPGNWriter(String)
	 */
	public XMLPGNWriter(Level loggerLevel){
		this(loggerLevel, DEFAULT_FILE_NAME+formatDate(),"");
	}
	
	/**
	 * Creates a new <code>XMLPGNWriter</code> with a specific name and a Level 
	 * for the Logger.
	 * 
	 * @param loggerLevel the <code>Level</code> of the Logger. It could be:<br>
	 * <ul><li>{@link Level#ALL}</li>
	 * <li>{@link Level#DEBUG}</li>
	 * <li>{@link Level#ERROR}</li>
	 * <li>{@link Level#WARN}</li>
	 * <li>{@link Level#FATAL}</li>
	 * <li>{@link Level#INFO}</li>
	 * <li>{@link Level#OFF}</li></ul>
	 * @param file the name of the file to create (without the extension)
	 * 
	 * @see Logger
	 * @see Level
	 */
	public XMLPGNWriter(Level loggerLevel, String file){
		this(loggerLevel,file,"");
	}

	/**
	 * Creates a new <code>XMLPGNWriter</code> with a specific name, a Level 
	 * for the Logger and an xml configuration file.
	 * 
	 * @param loggerLevel the <code>Level</code> of the Logger. It could be:<br>
	 * <ul><li>{@link Level#ALL}</li>
	 * <li>{@link Level#DEBUG}</li>
	 * <li>{@link Level#ERROR}</li>
	 * <li>{@link Level#WARN}</li>
	 * <li>{@link Level#FATAL}</li>
	 * <li>{@link Level#INFO}</li>
	 * <li>{@link Level#OFF}</li></ul>
	 * @param file the name of the file to create (without the extension)
	 * @param xmlConfigurationFile the xml configuration file used to configure the Logger.
	 * 
	 * @see Logger
	 * @see Level
	 * @see #setLoggerConfiguratorFile(String)
	 */
	public XMLPGNWriter(Level loggerLevel, String file, String xmlConfigurationFile){
		if(loggerLevel!=null){
			_loggerLevel=loggerLevel;
		}
		LOGGER.setLevel(_loggerLevel);
		setFileName(file);
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
	 * Returns the Level of the logger
	 * @return the Level of the logger
	 * @see Level
	 * @see Logger
	 */
	public Level getLoggerLevel(){
		return this._loggerLevel;
	}
	
	/**
	 * Sets the name of the file to write.
	 * @param filename the name of the file to write.
	 */
	public void setFileName(String filename){
		String fname=filename;
		if(filename.indexOf(".")>0){
			fname=filename.substring(0, filename.indexOf("."));
		}
		_filename=fname;
	}
	
	/**
	 * Returns the name of the file to write.
	 * @return the name of the file to write.
	 */
	public String getFileName(){return _filename+".xml";}
		
	/**
	 * Writes a PGNGame to a single xml file.
	 * @param game the PGNGame to write
	 * @see PGNGame
	 */
	public boolean writePGNGame(PGNGame game){
		List<PGNGame> l=null;
		if(game!=null){
			l=new ArrayList<PGNGame>();
			l.add(game);
		}
		return writePGNGames(l);
	}
	
	/**
	 * Writes a List of <code>PGNGame</code>s to an xml file.
	 * @param games a List of <code>PGNGame</code>s
	 * @see PGNGame
	 */
	public boolean writePGNGames(List<PGNGame> games){
		if(games!=null && games.size()>0){
			try {
				OutputStream out = new FileOutputStream(getFileName());
				XMLOutputFactory factory = XMLOutputFactory.newInstance();
				XMLStreamWriter writer = factory.createXMLStreamWriter(out);
				writer.writeStartDocument("UTF-8","1.0");
				writer.writeStartElement("games");
				// writing the games
				for(PGNGame game:games){
					writeToWriterPGNGame(writer,game);
				}			
				writer.writeEndElement();
				writer.writeEndDocument();
				writer.flush();
				writer.close();
				out.close();
				return true;
			} catch (FileNotFoundException e) {
				log("error in writing PGNGames ",e);
			} catch (XMLStreamException e) {
				log("error in writing PGNGames ",e);
			} catch (IOException e) {
				log("error in writing PGNGames ",e);
			}
		}
		return false;
	}
	
	/**
	 * Writes a PGNGame to the XMLStreamWriter
	 * @param writer the current XMLStreamWriter
	 * @param game the game to write
	 * @throws XMLStreamException
	 * @see XMLStreamWriter
	 */
	private void writeToWriterPGNGame(XMLStreamWriter writer, PGNGame game) 
		throws XMLStreamException{
		// the game
		writer.writeStartElement("game");
		// attributes of a game
		Set<String> attributes=game.getAttributes();
		if(attributes!=null){
			for(String s:attributes){
				writer.writeEmptyElement("game_property");
				customWriteAttribute(writer,"name",s);
				customWriteAttribute(writer,"value",game.getValue(s));
			}
		}
		// the hits
		writer.writeStartElement("hits");
		List<PGNHit> hits=game.getHitsList();
		int i=1;
		for(PGNHit hit:hits){
			writer.writeStartElement("hit");
			customWriteAttribute(writer, "number", Integer.toString(i));
			writer.writeCData(hit.getHit());
			writer.writeEndElement();// end of hit		
			i++;
		}
		writer.writeEndElement();// end of hits
		writer.writeEndElement();// end of game
	}
	
	/**
	 * Writes an attribute to the output stream without a prefix. If <code>value</code>
	 * is not null, it will write it, otherwise it will write an empty String
	 * 
	 * @param xmlWriter the current XmlStreamWriter
	 * @param localname the local name of the attribute
	 * @param value the value of the attribute
	 * 
	 * @throws XMLStreamException
	 * 
	 * @see XmlStreamSriter
	 */
	private void customWriteAttribute(XMLStreamWriter xmlWriter, String localname, String value)
		throws XMLStreamException{
		if(value!=null){
			xmlWriter.writeAttribute(localname, value);
		}else{
			xmlWriter.writeAttribute(localname, "");
		}
	}
	
	/**
	 * Formats a Date and returns it to a String representation using the
	 * {@link #DATEFORMAT} SimpleDateFormat pattern. A new Date is used at
	 * every call.
	 * 
	 * @return a String representation of a Date using the {@link #DATEFORMAT} 
	 * SimpleDateFormat pattern.
	 * 
	 * @see #formatDate(SimpleDateFormat)
	 */
	private synchronized static String formatDate(){
		return formatDate(DATEFORMAT);
	}
	
	/**
	 * Formats a Date and returns it to a String representation using the
	 * <code>sdf</code> SimpleDateFormat pattern. A new Date is used at
	 * every call.
	 * 
	 * @param sdf the SimpleDateFormat used for the formatting
	 * 
	 * @return a String representation of a Date using the <code>format</code> pattern
	 */
	private synchronized static String formatDate(SimpleDateFormat sdf){
		return sdf.format(new Date());
	}
	/**
	 * Logs the message using the Logger with the Level set.
	 * @param msg the message to log
	 */
	public void log(String msg){
		log(msg, null);
	}
	
	/**
	 * Logs the message and the Throwable using the Logger with the Level set 
	 * in the constructor.
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
