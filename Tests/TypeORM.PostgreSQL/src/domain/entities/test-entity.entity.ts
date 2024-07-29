import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('test_entity')
export class TestEntity {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column('bytea')
  binaryField: any;

  @Column('boolean')
  boolField: boolean;

  @Column('bytea')
  byteField: number;

  @Column('character')
  charField: string;

  @Column('date')
  dateField: Date;

  @Column('timestamp')
  datetimeField: Date;

  @Column('timestamp with time zone')
  datetimeoffsetField: any;

  @Column('decimal', { precision: 18, scale: 2 })
  decimalField: number;

  @Column('double precision')
  doubleField: number;

  @Column('real')
  floatField: number;

  @Column('uuid')
  guidField: string;

  @Column('integer')
  intField: number;

  @Column('bigint')
  longField: number;

  @Column('smallint')
  shortField: number;

  @Column('character varying')
  stringField: string;

  @Column('character varying', { nullable: true })
  createdBy?: string;

  @Column('timestamp', { nullable: true })
  createdDate?: Date;

  @Column('character varying', { nullable: true })
  lastModifiedBy?: string;

  @Column('timestamp', { nullable: true })
  lastModifiedDate?: Date;
}