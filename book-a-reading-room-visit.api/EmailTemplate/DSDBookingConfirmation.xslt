<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="Root">
    <style>
      ul {
      padding: 0;
      margin: 30px 0;
      }
      ul li {
      list-style-type: none;
      margin-bottom: 10px;
      padding-bottom: 10px;
      }
    </style>
    <body style="font-family: Arial, sans-serif;font-size: 16px;line-height: 24px;">
      <h3 style="margin-top: 2em;">Booking summary </h3>

      <ul>
        <li style="font-size: 32px;">
          Name: <b>
            <xsl:value-of select="Name" />
          </b>
        </li>
        <li>
          Date of visit: <b>
            <xsl:value-of select="VisitStartDate" />
          </b>
        </li>
        <li style="font-size: 32px;">
          Reading room: <b>
            <xsl:value-of select="ReadingRoom" />
          </b>
        </li>
        <li>
          Seat: 
        </li>
        <xsl:if test="string(Phone)">
          <li>
            Telephone: <xsl:value-of select="Phone" />
          </li>
        </xsl:if>
        <li>
          Reader's ticket number: <xsl:value-of select="ReaderTicket" />
        </li>
        <li>
          Booking reference: <xsl:value-of select="BookingReference" />
        </li>
      </ul>
      <hr />
      <h3 style="margin-top: 2em;">Document order</h3>

      <table>
        <tbody style="font-family: Arial, sans-serif;font-size: 16px;">
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[1]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[1]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[1]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[1]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[2]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[2]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[2]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[2]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[3]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[3]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[3]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[3]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[4]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[4]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[4]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[4]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[5]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[5]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[5]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[5]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder1[6]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder1[6]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="DocumentOrder2[6]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="DocumentOrder2[6]/Reference" />
            </td>
          </tr>
          <xsl:if test="string(DocumentOrder1[7]/Label)">
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[7]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[7]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[7]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[7]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[8]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[8]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[8]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[8]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[9]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[9]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[9]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[9]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[10]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[10]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[10]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[10]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[11]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[11]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[11]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[11]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[12]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[12]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[12]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[12]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[13]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[13]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[13]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[13]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[14]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[14]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[14]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[14]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[15]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[15]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[15]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[15]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[16]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[16]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[16]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[16]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[17]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[17]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[17]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[17]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[18]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[18]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[18]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[18]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[19]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[19]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[19]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[19]/Reference" />
              </td>
            </tr>
            <tr>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder1[20]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder1[20]/Reference" />
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <b>
                  <xsl:value-of select="DocumentOrder2[20]/Label" />
                </b>
              </td>
              <td style="padding: 0.5em;width: 200px;">
                <xsl:value-of select="DocumentOrder2[20]/Reference" />
              </td>
            </tr>
          </xsl:if>
        </tbody>
      </table>

      <table>
        <tbody style="font-family: Arial, sans-serif;font-size: 16px;">
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="ReserveDocumentOrder[1]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="ReserveDocumentOrder[1]/Reference" />
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="ReserveDocumentOrder[3]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="ReserveDocumentOrder[3]/Reference" />
            </td>
          </tr>
          <tr>
            <td style="padding: 0.5em;width: 200px;">
              <b>
                <xsl:value-of select="ReserveDocumentOrder[2]/Label" />
              </b>
            </td>
            <td style="padding: 0.5em;width: 200px;">
              <xsl:value-of select="ReserveDocumentOrder[2]/Reference" />
            </td>
            <td></td>
            <td></td>
          </tr>
        </tbody>
      </table>

      <xsl:if test="string(AdditionalRequirements)">
        <ul>
          <li>
            Access needs and research requests: <xsl:value-of select="AdditionalRequirements" />
          </li>
        </ul>
      </xsl:if>
    </body>
  </xsl:template>
</xsl:stylesheet>
