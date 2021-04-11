<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <body style="font-family:Arial;font-size:9pt">
      <h3>Booking summary </h3>
      <table>
        <tbody>
          <tr>
            <th>Name</th>
            <td>
              <xsl:value-of select="Name" />
            </td>
          </tr>
          <xsl:if test="string(Phone)">
            <tr>
              <th>Telephone</th>
              <td>
                <xsl:value-of select="Phone" />
              </td>
            </tr>
          </xsl:if>
          <tr>
            <th>Reader’s ticket number</th>
            <td>
              <xsl:value-of select="ReaderTicket" />
            </td>
          </tr>
          <tr>
            <th>Booking reference</th>
            <td>
              <xsl:value-of select="BookingReference" />
            </td>
          </tr>
          <tr>
            <th>Visit date</th>
            <td>
              <xsl:value-of select="VisitStartDate" />
            </td>
          </tr>
          <tr>
            <th>Seat number</th>
            <td>
              <xsl:value-of select="SeatNumber" />
            </td>
          </tr>
          <xsl:if test="string(AdditionalRequirements)">
            <tr>
              <th>Access needs and research requests</th>
              <td>
                <xsl:value-of select="AdditionalRequirements" />
              </td>
            </tr>
          </xsl:if>
        </tbody>
      </table>

      <hr />
      <h3>Document order</h3>

      <xsl:for-each select="DocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>

      <xsl:for-each select="ReserveDocumentOrder">
        <p>
          <b>
            <xsl:value-of select="Label" />
          </b>
          <xsl:value-of select="Document" />
        </p>
      </xsl:for-each>
    </body>
  </xsl:template>
</xsl:stylesheet>
