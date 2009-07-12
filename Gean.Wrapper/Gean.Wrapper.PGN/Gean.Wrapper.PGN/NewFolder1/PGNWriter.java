/*
 * PGNWriter.java
 * 
 * Copyright 2008 supareno 
 *  
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at
 *  
 * 		http://www.apache.org/licenses/LICENSE-2.0 
 *  
 * Unless required by applicable law or agreed to in writing, software 
 * distributed under the License is distributed on an "AS IS" BASIS, 
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
 * See the License for the specific language governing permissions and 
 * limitations under the License.
 */
package com.supareno.pgnparser;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.*;

/**
 * The <code>PGNWriter</code> class is used to write pgn files from <code>PGNGame</code>(s).
 * 
 * @author supareno
 * 
 * @see PGNGame
 */
public class PGNWriter {

	/**
	 * Writes the <code>game</code> to a well-formed PGN file called <code>filename</code>.
	 * If the game is <code>null</code>, it throws a <code>NullPointerException</code>.<br>
	 * If an error occurs during the writing, it throws an <code>IOException</code>.
	 * 
	 * @param game the PGNGame to write
	 * @param filename the name of the pgn file to write without extension
	 * 
	 * @throws NullPointerException if game == null
	 * @throws IOException if the file is empty or if the path to the file is incorrect
	 * or if an error occurs during the writing
	 * 
	 * @see PGNGame
	 */
	public static void writePGNFile(PGNGame game, String filename) 
		throws NullPointerException, IOException{
		if(game==null){
			throw new NullPointerException("the PGNGame is null");
		}
		List<PGNGame> games=new ArrayList<PGNGame>();
		games.add(game);
		writePGNFile(games, filename);
	}
	
	/**
	 * Writes the <code>games</code> to a well-formed PGN file called <code>filename</code>.
	 * If the games is <code>null</code>, it throws a <code>NullPointerException</code>.<br>
	 * If an error occurs during the writing, it throws an <code>IOException</code>.
	 * 
	 * @param games the List of PGNGame to write
	 * @param filename the name of the pgn file to write without extension
	 * 
	 * @throws NullPointerException if games == null
	 * @throws IOException if the file is empty or if the path to the file is incorrect
	 * or if an error occurs during the writing
	 * 
	 * @see PGNGame
	 */
	public static void writePGNFile(List<PGNGame> games, String filename) 
		throws NullPointerException, IOException{
		String fname=filename;
		if(filename.indexOf(".")>0){
			fname=filename.substring(0, filename.indexOf("."));
		}
		if(games==null){
			throw new NullPointerException("the List of PGNGame is null");
		}
		BufferedWriter out = new BufferedWriter(new FileWriter(fname+".pgn"));
		for(PGNGame game:games){
			if(game==null){continue;}
			Set<String> attributes=game.getAttributes();
			if(attributes!=null){
				for(String attribute:attributes){
					String stringToWrite="["+attribute+" \""+game.getValue(attribute)+"\"]";
					out.write(stringToWrite);
					out.write("\n");
				}
				List<PGNHit> hits=game.getHitsList();
				if(hits!=null && hits.size()>0){
					for(int i=1;i<=hits.size();i++){
						out.write(i+"."+hits.get(i-1).getHit());
						out.write(" ");
					}
					out.write(game.getValue("Result"));
				}
			}
		}
		out.close();	    
	}
}
