/*
 * PGNGame.java
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

import java.io.Serializable;
import java.util.*;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

/**
 * The <code>PGNGame</code> class is a object that represents a PGN game with
 * all the general attributes and a List of <code>PGNHit</code>s. The attributes 
 * are stored in a <code>Map</code> and can be accessible via {@link #getAttributesNValues()}.
 * <br><br>
 * The general attributes are:<br>
 * <b>Event:</b><br>
 * The name of the tournament or match event
 * <br>
 * <b>Site:</b><br>
 * The location of the event. This is in "City, Region COUNTRY" format, where COUNTRY 
 * is the 3-letter International Olympic Committee code for the country. An example 
 * is "New York City, NY USA". But it could simply be "USA".
 * <br>
 * <b>Date:</b><br>
 * The starting date of the game, in YYYY.MM.DD form. "??" are used for unknown values.
 * <br>
 * <b>Round:</b><br>
 * The playing round ordinal of the game within the event
 * <br>
 * <b>White:</b><br>
 * The player of the white pieces, in "last name, first name" format.
 * <br>
 * <b>Black:</b><br>
 * The player of the black pieces, in "last name, first name" format.
 * <br>
 * <b>Result:</b><br>
 * This parameter can only have four possible values:<br>
 * <ul><li>"1-0" (White won)</li>
 * <li>"0-1" (Black won)</li>
 * <li>"1/2-1/2" (Draw)</li>
 * <li>"*" (other, e.g., the game is ongoing)</li></ul>
 * 
 * For more details about PGN format, check out this link:<br>
 * {@linkplain http://chess.about.com/library/weekly/aa101202a.htm}
 * 
 * @author supareno
 *
 * @see PGNHit
 */
public class PGNGame implements Comparable<PGNGame>, Serializable{
	
	// used for serialization
	private static final long serialVersionUID = 421L;
	
	private Map<String,String> attributes=null;		
	private List<PGNHit> hitsList=null;
	
	/**
	 * Empty constructor
	 */
	public PGNGame(){
		this.attributes=new LinkedHashMap<String,String>();
		this.hitsList=new ArrayList<PGNHit>();
	}
	
	/**
	 * Sets a List of <code>PGNHit</code>
	 * @param list a List of <code>PGNHit</code>
	 */
	public void setPGNHits(List<PGNHit> list){
		this.hitsList=list;
	}
	
	/**
	 * Adds a <code>PGNHit</code> to the <code>PGNGame</code>
	 * @param hit the <code>PGNHit</code> to add to the <code>PGNGame</code>
	 * @see PGNHit
	 */
	public void addPGNHit(PGNHit hit){
		if(hit!=null){
			this.hitsList.add(hit);
		}
	}
	
	/**
	 * Returns the List of the <code>PGNHit</code> associated to the <code>PGNGame</code>
	 * @return the List of the <code>PGNHit</code> associated to the <code>PGNGame</code>
	 */
	public List<PGNHit> getHitsList(){return this.hitsList;}
	
	/**
	 * Returns the PGNHit associated to the position <code>hitnumber</code> if
	 * this position exists. 
	 * @param hitnumber the position of the hit in the list
	 * @return the PGNHit associated to the position <code>hitnumber</code>
	 */
	public PGNHit getHit(int hitnumber){
		if(hitnumber<this.hitsList.size()){
			return this.hitsList.get(hitnumber);
		}
		return null;
	}
	
	/**
	 * Adds an attribute and his value in the attributes Map of the PGNGame
	 * @param attribute the name of the attribute
	 * @param value the value of the attribute
	 */
	public void addAttributeNValue(String attribute, String value){
		if(attribute!=null){
			this.attributes.put(attribute, value);
		}
	}
	
	/**
	 * Returns a <code>Map&lt;String,String></code> that contains the tags pairs of 
	 * attributes and values. Thsi Map corresponding of the element
	 * @return a <code>Map&ltString,String></code> that contains the tags pairs of 
	 * attributes and values
	 */
	public Map<String,String> getAttributesNValues(){
		return new LinkedHashMap<String,String>(this.attributes);
	}
	
	/**
	 * Returns a <code>Set&lt;String></code> that contains all the attributes of
	 * the PGNGame
	 * @return a <code>Set&lt;String></code> that contains all the attributes of
	 * the PGNGame
	 */
	public Set<String> getAttributes(){
		return this.attributes.keySet();
	}
	
	/**
	 * Returns a <code>Collection&lt;String></code> that contains all the values associated
	 * to the attributes of the PGNGame
	 * @return a <code>Collection&lt;String></code> that contains all the values associated
	 * to the attributes of the PGNGame
	 */
	public Collection<String> getValues(){
		return this.attributes.values();
	}
	
	/**
	 * Returns the value associated to the <code>attribute</code> parameter
	 * @param attribute the value of the attribute
	 * @return the value associated to the <code>attribute</code> parameter
	 */
	public String getValue(String attribute){
		return this.attributes.get(attribute);
	}
	

	@Override
	public int compareTo(PGNGame obj) {
		return CompareToBuilder.reflectionCompare(this, obj);
	}

	@Override
	public boolean equals(Object obj) {
		return EqualsBuilder.reflectionEquals(this, obj);
	}

	@Override
	public int hashCode() {
		return HashCodeBuilder.reflectionHashCode(this);
	}
	
	@Override
	public String toString() {
		return ToStringBuilder.reflectionToString(this);
	}
}
