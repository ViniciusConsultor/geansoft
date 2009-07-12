/*
 * PGNHit.java
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
import org.apache.commons.lang.builder.*;

/**
 * The <code>PGNHit</code> class represents a PGN hit in the international rule.
 * For more details about PGN format, check out this link:<br>
 * {@linkplain http://chess.about.com/library/weekly/aa101202a.htm}
 * 
 * @author supareno
 */
public class PGNHit implements Comparable<PGNHit>, Serializable {

	// used for serialization
	private static final long serialVersionUID = 411L;
	
	private String _hit=null;
	
	/**
	 * Empty constructor
	 */
	public PGNHit(){
		this("");
	}
	
	/**
	 * Constructs a new <code>PGNHit</code> with a specific hit
	 * @param hit a new <code>PGNHit</code> with a specific hit
	 */
	public PGNHit(String hit){
		_hit=hit;
	}
	
	/**
	 * Returns the <code>PGNHit</code> hit associated to this object
	 * @return the <code>PGNHit</code> hit associated to this object
	 */
	public String getHit(){
		return _hit;
	}
	
	/**
	 * Returns a String array of the hit
	 * @return a String array of the hit
	 */
	public String[] getHitSeparated(){
		return _hit.split(" ");
	}
	
	/**
	 * Sets the hit to the <code>PGNHit</code>
	 * @param hit the hit associated to this object
	 */
	public void setHit(String hit){
		_hit=hit;
	}

	@Override
	public int compareTo(PGNHit obj) {
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
